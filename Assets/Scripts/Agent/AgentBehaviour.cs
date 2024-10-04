using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.Animations;
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

        public GameplayAction CurrentAction => _agent.Actions.Peek();
        public int ActionCount => _agent.Actions.Count;

        public Agent Agent => _agent;
        private Agent _agent = new();

        private Animator _animator;

        private void Start(){
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _planner = GetComponent<AgentPlanner>();

            CurrentState = new AgentIdle(this);
            _agent = new();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update(){
            _currentState.Update();
            _currentState.CheckForTransitions();
            _animator.SetFloat("Current Speed", _navMeshAgent.velocity.magnitude);
        }

        public void PlanActions(Goal goal)
        {
            _planner.SetGoal(goal);
            _planner.CalculatePath();
        }
        
        public void SetGoal(Goal goal)
        {
            _agent.CurrentGoal = goal;
        }

        public void TryUseAnimation(string animationName)
        {
            _animator.Play(animationName);
        }
    }
}