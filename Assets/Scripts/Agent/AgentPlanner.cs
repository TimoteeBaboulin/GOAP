using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Agent{
    [RequireComponent(typeof(Agent))]
    public class AgentPlanner : MonoBehaviour{
        [SerializeField] private List<Action> _actions;

        private Agent _agent;
        [SerializeField]private Goal _currentGoal;

        private void Start(){
            _agent = GetComponent<Agent>();
        }

        public void SetGoal(Goal goal){
            _currentGoal = goal;
        }

        public bool CalculatePath(){
            if (_agent.CurrentPrerequisites.Contains(_currentGoal.Prerequisite.effectName))
                return false;
            
            //Initialise variables for the function
            List<Effect> effects = new();
            effects.Add(_currentGoal.Prerequisite);
            Node baseNode = new Node(_currentGoal);
            Dictionary<Action, Node> nodes = new Dictionary<Action, Node>();
            nodes.Add(_currentGoal, baseNode);
            
            //Generate the tree
            GenerateGraph(effects, nodes, baseNode);
            
            //Pathfinding
            List<Node> path = Djikstra(nodes.Values.ToList(), baseNode);

            Stack<Action> actions = new Stack<Action>();

            for (int x = path.Count - 1; x >= 0; x--){
                actions.Push(path[x].Action);
            }
            _agent.Actions = actions;
            _agent.CurrentGoal = _currentGoal;
            return true;
        }

        private void GenerateGraph(List<Effect> prerequisites, Dictionary<Action, Node> nodes, Node lastNode){
            if (CheckPrerequisites(prerequisites)) return;

            foreach (var action in _actions){
                List<Effect> newPrerequisites = new List<Effect>(prerequisites);
                
                int count = 0;
                foreach (var effect in action.Effects){
                    if (!prerequisites.Contains(effect)) continue;

                    count++;
                    newPrerequisites.Remove(effect);
                }
                
                if (count == 0) continue;
                foreach (var prerequisite in action.Prerequisites){
                    if (!prerequisites.Contains(prerequisite))
                        newPrerequisites.Add(prerequisite);
                }

                Node node;
                if (!nodes.ContainsKey(action)){
                    node = new Node(action);
                    nodes.Add(action, node);
                }
                else
                    node = nodes[action];
                
                node.Parent.Add(lastNode);
                lastNode.Children.Add(node);
                
                GenerateGraph(newPrerequisites, nodes, node);
            }
        }
        private bool CheckPrerequisites(List<Effect> prerequisites){
            foreach (var prerequisite in prerequisites){
                switch (prerequisite.value){
                    case true:
                        if (!_agent.CurrentPrerequisites.Contains(prerequisite.effectName)) return false;
                        break;
                    case false:
                        if (_agent.CurrentPrerequisites.Contains(prerequisite.effectName)) return false;
                        break;
                }
            }

            return true;
        }

        private List<Node> Djikstra(List<Node> nodes, Node start){
            List<Node> unvisited = new List<Node>(nodes);
            Node[] leafNodes = unvisited.Where(obj => obj.Children.Count == 0).ToArray();
            Node current = start;
            Dictionary<Node, Node> previous = new();
            
            foreach (var node in unvisited){
                node.value = node == start ? 0 : Int32.MaxValue;
                previous.Add(node, null);
            }

            while (unvisited.Any(obj=> obj.value != Int32.MaxValue)){
                foreach (var child in current.Children){
                    if (child.value > current.value + child.Cost){
                        previous[child] = current;
                        child.value = current.value + child.Cost;
                    }
                }

                unvisited.Remove(current);
                unvisited.OrderBy(obj => obj.value);
                if (unvisited.Count > 0)
                    current = unvisited[0];
            }

            List<Node> bestPath = null;
            int bestValue = Int32.MaxValue;
            foreach (var leaf in leafNodes){
                List<Node> path = new();
                current = leaf;
                int value = current.Cost;
                path.Add(current);
                while (previous[current] != null){
                    current = previous[current];
                    value += current.Cost;
                    path.Add(current);
                }

                if (current != start)
                    continue;
                
                if (value < bestValue){
                    bestValue = value;
                    bestPath = path;
                }
            }

            return bestPath;
        }


        [ContextMenu("CheckGraph")]
        public void CheckGraph(){
            CalculatePath();
        }
    }
}