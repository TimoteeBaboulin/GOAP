using UnityEngine;

namespace Agent{
    public class AgentMoveTo : AgentState{
        public AgentMoveTo(Agent agent) : base(agent){ }
        public override void Enter(){
            _agent.NavMeshAgent.SetDestination(_agent.Actions.Peek().Position);
        }

        public override void Update(){
            Debug.Log("Moving");
        }

        public override void Exit(){
            _agent.NavMeshAgent.SetDestination(_agent.Transform.position);
        }

        public override void CheckForTransitions(){
            if (_agent.NavMeshAgent.remainingDistance > _agent.NavMeshAgent.stoppingDistance) return;

            _agent.CurrentState = new AgentPerformAction(_agent);
        }
    }
}