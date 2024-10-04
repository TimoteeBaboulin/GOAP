using System;
using System.Collections.Generic;
using Agent;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameplayAction : MonoBehaviour
    {
        public static List<AgentPlanner> agentPlanners = new List<AgentPlanner>();

        public Prerequisite[] Prerequisites
        {
            get { return _prerequisites; }
            set { _prerequisites = value; }
        }
        
        [SerializeField] private Prerequisite[] _prerequisites;
        
        public Effect[] Effects => _effects;
        [SerializeField] private Effect[] _effects = { };
        
        public virtual Vector3 Position => transform.position;

        [SerializeField] private float _movementCost;
        public float Cost => _cost;
        [SerializeField] private float _cost;

        public float GetCost(Vector3 _position)
        {
            return _cost + (float)Math.Round(_movementCost * Vector3.Distance(_position, transform.position));
        }

        public virtual void OnStartUse(AgentBehaviour _agent)
        {
            return;
        }
        
        public virtual void OnEndUse(AgentBehaviour _agent)
        {
            return;
        }
        
        private void Start()
        {
            foreach (var planner in agentPlanners)
            {
                planner.AddAction(this);
            }
        }
        
        private void OnEnable()
        {
            foreach (var planner in agentPlanners)
            {
                planner.AddAction(this);
            }
        }

        private void OnDisable()
        {
            foreach (var planner in agentPlanners)
            {
                planner.RemoveAction(this);
            }
        }
    }
}