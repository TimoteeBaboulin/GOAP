using System.Collections.Generic;
using UnityEngine;

namespace Agent{
    public class AgentPerformAction : AgentState{
        private float _time;
        private Effect2[] _effects;

        public AgentPerformAction(AgentBehaviour agent) : base(agent){ }
        
        public override void Enter(){
            _effects = _agent.CurrentAction.Effects;
            _time = Time.realtimeSinceStartup + _agent.CurrentAction.Cost;
        }

        public override void Update(){
        }

        public override void Exit(){
            foreach(var effect in _effects)
            {
                effect.Activate(_agent.Agent);
            }
            _agent.Agent.Actions.Pop();
        }

        public override void CheckForTransitions(){
            if (Time.realtimeSinceStartup < _time) return;

            if (_agent.ActionCount <= 1){
                _agent.SetGoal(null);
                _agent.CurrentState = new AgentIdle(_agent);
            }
            else{
                _agent.CurrentState = new AgentMoveTo(_agent);
            }
        }
    }
}