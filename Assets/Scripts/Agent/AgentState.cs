using System;

namespace Agent{
    public abstract class AgentState{
        protected AgentBehaviour _agent;
        
        protected AgentState(AgentBehaviour agent){
            if (agent == null) throw new NullReferenceException("Agent is null in state initialization.");
            _agent = agent;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();

        public abstract void CheckForTransitions();
    }
}