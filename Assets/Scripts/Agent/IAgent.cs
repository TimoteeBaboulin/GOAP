﻿using System.Collections;
using System.Collections.Generic;

namespace Agent
{
    public interface IAgent
    {
        public EntityInventory Inventory { get; }
        public Goal CurrentGoal { get; set; }

        public void SetActions(Action[] actions);
        public IAgent Clone();
    }

    public class Agent
        : IAgent
    {
        public EntityInventory Inventory { get { return _inventory; } }
        private EntityInventory _inventory;

        public Goal CurrentGoal {
            get { return _currentGoal; }
            set { _currentGoal = value; }
        }
        private Goal _currentGoal;

        public Stack<Action> Actions => _actions;
        private Stack<Action> _actions = new();

        public void SetActions(Action[] actions)
        {
            _actions.Clear();

            foreach (var action in actions)
            {
                _actions.Push(action);
            }
        }

        public IAgent Clone()
        {
            return new Agent(this);
        }

        public Agent()
        {
            _inventory = new();
            _currentGoal = null;
            _actions = new();
        }

        private Agent(Agent agent)
        {
            _inventory = new EntityInventory(agent._inventory);
            _currentGoal = agent._currentGoal;
            _actions = agent._actions;
        }
    }
}