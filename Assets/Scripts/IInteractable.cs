using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Vector3 Position { get; }
    bool Interact(Agent.AgentBehaviour agent);
}

public interface ITimedInteractable : IInteractable
{

    bool IsFinished(Agent.AgentBehaviour agent);
    void UpdateInteraction();
}

public interface IInteractableManager
{
    public IInteractable GetInteractable(Vector3 position);
}

public interface ITimedInteractableManager
{
    public ITimedInteractable GetTimedInteractable
}