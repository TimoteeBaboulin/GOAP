using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Agent{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour{
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

        public List<string> CurrentPrerequisites;
        public Goal CurrentGoal;
        public Stack<Action> Actions = new();
        public Action[] StartupActions;

        private void Start(){
            _navMeshAgent = GetComponent<NavMeshAgent>();
            CurrentGoal = null;

            CurrentState = new AgentIdle(this);
            if (StartupActions.Length == 0) return;
            for (int x = StartupActions.Length - 1; x >= 0; x--){
                Actions.Push(StartupActions[x]);
            }
        }

        private void Update(){
            _currentState.Update();
            _currentState.CheckForTransitions();
        }
    }
}