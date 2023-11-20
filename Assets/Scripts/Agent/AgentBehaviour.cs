using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentBehaviour : MonoBehaviour
    {
        public AgentState CurrentState{
            get => _currentState;
            set{
                _currentState?.Exit();
                _currentState = value;
                _currentState.Enter();
            }
        }
        private AgentState _currentState;

        public Transform Transform => transform;
        
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        private NavMeshAgent _navMeshAgent;

        private AgentPlanner _planner;

        public Action CurrentAction => _agent.Actions.Peek();
        public int ActionCount => _agent.Actions.Count;

        public Agent Agent => _agent;
        private Agent _agent = new();

        private void Start(){
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _planner = GetComponent<AgentPlanner>();

            CurrentState = new AgentIdle(this);
            _agent = new();
        }

        private void Update(){
            _currentState.Update();
            _currentState.CheckForTransitions();
        }

        [ContextMenu("CheckGoal")]
        public void CheckGoal(){
            _planner.CalculatePath();
        }

        public void SetGoal(Goal goal)
        {
            _agent.CurrentGoal = goal;
        }
    }
}