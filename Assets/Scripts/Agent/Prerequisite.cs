using UnityEngine;

public abstract class Prerequisite : ScriptableObject
{
    public abstract bool IsValid(Agent.IAgent agent);
}

/*public struct Prerequisite
{
    IEffect Effect;
    bool Value;

    bool IsValid(Agent.Agent agent) { return Effect.IsValid(agent); }
}*/