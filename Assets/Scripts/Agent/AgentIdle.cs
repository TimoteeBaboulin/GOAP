using System;
using UnityEngine;

namespace Agent{
    public class AgentIdle : AgentState{
        public AgentIdle(Agent agent) : base(agent){ }
        
        public override void Enter(){
            _agent.NavMeshAgent.SetDestination(_agent.Transform.position);
        }

        public override void Update(){
            Debug.Log("Idle");
        }

        public override void Exit(){ }

        public override void CheckForTransitions(){
            if (_agent.CurrentGoal == null) return;

            if (_agent.Actions.Count == 0)
                throw new IndexOutOfRangeException("Agent goal is not null despite not having any actions");
            
            _agent.CurrentState = new AgentMoveTo(_agent);
        }
    }
}