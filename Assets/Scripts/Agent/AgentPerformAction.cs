using System.Collections.Generic;
using UnityEngine;

namespace Agent{
    public class AgentPerformAction : AgentState{
        private float _time;
        private Effect[] _effects;

        public AgentPerformAction(Agent agent) : base(agent){ }
        
        public override void Enter(){
            _effects = _agent.Actions.Peek().Effects;
            _time = Time.realtimeSinceStartup + _agent.Actions.Pop().Cost;
        }

        public override void Update(){
            Debug.Log("Action");
        }

        public override void Exit(){
            List<string> prerequisites = _agent.CurrentPrerequisites;
            foreach (var effect in _effects){
                switch (effect.value){
                    case true:
                        prerequisites.Add(effect.effectName);
                        break;
                    case false:
                        if (prerequisites.Contains(effect.effectName))
                            prerequisites.Remove(effect.effectName);
                        break;
                }
            }
        }

        public override void CheckForTransitions(){
            if (Time.realtimeSinceStartup < _time) return;

            if (_agent.Actions.Count == 0){
                _agent.CurrentGoal = null;
                _agent.CurrentState = new AgentIdle(_agent);
            }
            else{
                _agent.CurrentState = new AgentMoveTo(_agent);
            }
        }
    }
}