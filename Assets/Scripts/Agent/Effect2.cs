using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public abstract class Effect2 : ScriptableObject
{
    public abstract void Activate(Agent.IAgent agent);
}

/*public struct Prerequisite
{
    IEffect Effect;
    bool Value;

    bool IsValid(Agent.Agent agent) { return Effect.IsValid(agent); }
}*/