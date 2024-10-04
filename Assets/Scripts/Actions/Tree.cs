using Agent;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Tree : GameplayAction
{
    private static List<Tree> _trees = new();

    [SerializeField] private Item _wood;
    [SerializeField] private int _amount;

    private Agent.AgentBehaviour _agent;
    private float _time;
    private float _interactionTime = 1.5f;
    private bool _finished = false;

    public static Tree GetTree(Vector3 pos)
    {
        if (_trees.Count == 0)
            return null;

        return _trees.OrderBy(tree => Vector3.Distance(tree.transform.position, pos)).FirstOrDefault();
    }

    private void OnEnable()
    {
        _trees.Add(this);
    }

    private void OnDisable()
    {
        _trees.Remove(this);
    }

    public override void OnEndUse(AgentBehaviour _agent)
    {
        _agent.Agent.Inventory.AddItem(_wood, _amount);
    }
    
    public bool IsFinished(AgentBehaviour agent)
    {
        return _finished;
    }
}