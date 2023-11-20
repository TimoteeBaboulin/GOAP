using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Agent{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AgentPlanner : MonoBehaviour{
        [SerializeField] private List<Action2> _actions;

        private AgentBehaviour _agentBehaviour;
        [SerializeField]private Goal2 _currentGoal;

        private void Start(){
            _agentBehaviour = GetComponent<AgentBehaviour>();
        }

        public void SetGoal(Goal2 goal){
            _currentGoal = goal;
        }

        public bool CalculatePath(){
            if (_currentGoal.Goal.IsValid(_agentBehaviour.Agent))
                return false;
            
            //Initialise variables for the function
            List<Prerequisite> preconditions = new();
            preconditions.Add(_currentGoal.Goal);
            Node2 baseNode = new Node2(_currentGoal);
            Dictionary<Action2, Node2> nodes = new Dictionary<Action2, Node2>();
            nodes.Add(_currentGoal, baseNode);

            //Generate the tree
            IAgent newAgent = _agentBehaviour.Agent.Clone();
            GenerateGraph2(preconditions, nodes, baseNode, newAgent);
            
            //Pathfinding
            List<Node2> path = Djikstra(nodes.Values.ToList(), baseNode);

            Stack<Action2> actions = new Stack<Action2>();

            for (int x = 0; x < path.Count; x++){
                actions.Push(path[x].Action);
            }
            _agentBehaviour.Agent.SetActions(actions.ToArray());
            _agentBehaviour.Agent.CurrentGoal = _currentGoal;
            Debug.Log(actions.Count);
            return true;
        }

       /* private void GenerateGraph(List<Effect> prerequisites, Dictionary<Action, Node> nodes, Node lastNode){
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
        }*/

        private void GenerateGraph2(List<Prerequisite> prerequisites, Dictionary<Action2, Node2> nodes, Node2 lastNode, IAgent agent)
        {
            for (int x = prerequisites.Count - 1; x >= 0; x--)
            {
                if (!prerequisites[x].IsValid(agent)) continue;

                prerequisites.RemoveAt(x);
            }

            if (prerequisites.Count == 0) return;

            foreach (var action in _actions)
            {
                List<Prerequisite> newPrerequisites = new List<Prerequisite>(prerequisites);

                int count = 0;
                IAgent newAgent = agent.Clone();
                foreach (var effect in action.Effects)
                {
                    effect.Activate(newAgent);

                    /*if (!prerequisites.Contains(effect)) continue;

                    count++;
                    newPrerequisites.Remove(effect);*/
                }

                for (int x = newPrerequisites.Count - 1; x >= 0; x--)
                {
                    if (!prerequisites[x].IsValid(newAgent)) continue;

                    newPrerequisites.RemoveAt(x);
                    count++;
                }

                if (count == 0) continue;
                foreach (var prerequisite in action.Prerequisites)
                {
                    if (!prerequisite.IsValid(newAgent))
                        newPrerequisites.Add(prerequisite);
                }

                Node2 node;
                if (!nodes.ContainsKey(action))
                {
                    node = new Node2(action);
                    nodes.Add(action, node);
                }
                else
                    node = nodes[action];

                node.Parent.Add(lastNode);
                lastNode.Children.Add(node);

                GenerateGraph2(newPrerequisites, nodes, node, newAgent);
            }
        }

        /*private bool CheckPrerequisites(List<Effect> prerequisites){
            foreach (var prerequisite in prerequisites){
                switch (prerequisite.value){
                    case true:
                        if (!_agentBehaviour.CurrentPrerequisites.Contains(prerequisite.effectName)) return false;
                        break;
                    case false:
                        if (_agentBehaviour.CurrentPrerequisites.Contains(prerequisite.effectName)) return false;
                        break;
                }
            }

            return true;
        }*/

        private List<Node2> Djikstra(List<Node2> nodes, Node2 start){
            List<Node2> unvisited = new List<Node2>(nodes);
            Node2[] leafNodes = unvisited.Where(obj => obj.Children.Count == 0).ToArray();
            Node2 current = start;
            Dictionary<Node2, Node2> previous = new();
            
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

            List<Node2> bestPath = null;
            int bestValue = Int32.MaxValue;
            foreach (var leaf in leafNodes){
                List<Node2> path = new();
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