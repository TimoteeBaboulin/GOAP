using System;
using UnityEngine;

namespace Agent{
    public class AgentIdle : AgentState{
        public AgentIdle(AgentBehaviour agent) : base(agent){ }
        
        public override void Enter(){
            _agent.NavMeshAgent.SetDestination(_agent.Transform.position);
        }

        public override void Update(){
        }

        public override void Exit(){ }

        public override void CheckForTransitions(){
            if (!_agent.Agent.CurrentGoal) return;

            if (_agent.ActionCount == 0)
                throw new IndexOutOfRangeException("Agent goal is not null despite not having any actions");
            
            _agent.CurrentState = new AgentMoveTo(_agent);
        }
    }
}