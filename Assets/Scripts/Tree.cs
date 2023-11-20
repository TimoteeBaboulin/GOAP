using Agent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tree : MonoBehaviour, ITimedInteractable
{
    private static List<Tree> _trees = new();

    [SerializeField] private Item _wood;

    public Vector3 Position => transform.position;

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

    public bool IsFinished(AgentBehaviour agent)
    {
        return _finished;
    }

    public void UpdateInteraction()
    {
        if (Time.time >= _time)
        {
            _agent.Agent.Inventory.AddItem(_wood, 2);
            _finished = true;
            gameObject.SetActive(false);
        }
    }

    public bool Interact(AgentBehaviour agent)
    {
        _time = Time.time + _interactionTime;
        _agent = agent;

        return true;
    }
}