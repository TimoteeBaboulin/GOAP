using System.Collections;
using System.Collections.Generic;
using Agent;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _key;
    private bool _locked = true;

    public Vector3 Position => transform.position;

    public bool Interact(Agent.AgentBehaviour agent)
    {
        if (agent == null) return false;
        if (agent.Agent.Inventory.ContainsItem(_key))
        {
            _locked = false;
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public bool IsFinished(AgentBehaviour agent)
    {
        return true;
    }

    public void UpdateInteraction()
    {
        return;
    }
}
