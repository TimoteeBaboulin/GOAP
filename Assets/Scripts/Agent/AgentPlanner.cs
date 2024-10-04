using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Agent{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AgentPlanner : MonoBehaviour{
        [SerializeField] private List<Action> _actions;
        [SerializeField] private List<GameplayAction> _gameplayActions;

        private AgentBehaviour _agentBehaviour;
        [SerializeField]private Goal _currentGoal;

        private void Awake()
        {
            GameplayAction.agentPlanners.Add(this);
        }

        private void Start(){
            _agentBehaviour = GetComponent<AgentBehaviour>();
            
        }

        public void SetGoal(Goal goal){
            _currentGoal = goal;
        }

        public void AddAction(GameplayAction action)
        {
            if (_gameplayActions.Contains(action)) return;
            _gameplayActions.Add(action);
        }
        
        public void RemoveAction(GameplayAction action)
        {
            if (!_gameplayActions.Contains(action)) return;
            _gameplayActions.Remove(action);
        }

        public bool CalculatePath(){
            if (_currentGoal.Required.IsValid(_agentBehaviour.Agent))
                return false;
            
            //Initialise variables for the function
            List<Prerequisite> preconditions = new();
            preconditions.Add(_currentGoal.Required);
            Node baseNode = new Node(_currentGoal);
            Dictionary<GameplayAction, Node> nodes = new Dictionary<GameplayAction, Node>();

            //Generate the tree
            IAgent newAgent = _agentBehaviour.Agent.Clone();
            GenerateGraph2(preconditions, nodes, baseNode, newAgent);
            
            //Pathfinding
            List<Node> nodeList = nodes.Values.ToList();
            nodeList.Add(baseNode);
            List<Node> path = Djikstra(nodeList, baseNode);

            Stack<GameplayAction> actions = new Stack<GameplayAction>();

            for (int x = 0; x < path.Count; x++){
                actions.Push(path[x].Action);
            }
            _agentBehaviour.Agent.SetActions(actions.ToArray());
            _agentBehaviour.Agent.CurrentGoal = _currentGoal;
            return true;
        }

        private void GenerateGraph2(List<Prerequisite> prerequisites, Dictionary<GameplayAction, Node> nodes, Node lastNode, IAgent agent)
        {
            for (int x = prerequisites.Count - 1; x >= 0; x--)
            {
                if (!prerequisites[x].IsValid(agent)) continue;

                prerequisites.RemoveAt(x);
            }

            if (prerequisites.Count == 0) return;

            foreach (var action in _gameplayActions)
            {
                List<Prerequisite> newPrerequisites = new List<Prerequisite>(prerequisites);

                int count = 0;
                IAgent newAgent = agent.Clone();
                bool effectBlock = false;
                foreach (var effect in action.Effects)
                {
                    effect.Activate(newAgent);
                }

                if (effectBlock) continue;

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

                Node node;
                if (!nodes.ContainsKey(action))
                {
                    node = new Node(action);
                    nodes.Add(action, node);
                }
                else
                    node = nodes[action];

                node.Parent.Add(lastNode);
                lastNode.Children.Add(node);

                GenerateGraph2(newPrerequisites, nodes, node, newAgent);
            }
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
                    if (child.value > current.value + child.GetCost(current.isGoal ? child.Position : current.Position)){
                        previous[child] = current;
                        child.value = current.value + child.GetCost(current.isGoal ? child.Position : current.Position);
                    }
                }

                unvisited.Remove(current);
                unvisited.OrderBy(obj => obj.value);
                if (unvisited.Count > 0)
                    current = unvisited[0];
            }

            List<Node> bestPath = null;
            float bestValue = float.MaxValue;
            foreach (var leaf in leafNodes){
                List<Node> path = new();
                current = leaf;
                Vector3 position = previous[current] == null ? transform.position :
                    previous[current].isGoal ? current.Position : previous[current].Position;
                float value = current.GetCost(position);
                path.Add(current);
                while (previous[current] != null){
                    current = previous[current];
                    position = previous[current] == null ? transform.position :
                        previous[current].isGoal ? current.Position : previous[current].Position;
                    value += current.GetCost(position);
                    path.Add(current);
                }

                if (current != start)
                    continue;
                else
                {
                    path.Remove(current);
                }
                
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