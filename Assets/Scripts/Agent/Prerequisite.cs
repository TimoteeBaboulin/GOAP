using UnityEngine;

public abstract class Prerequisite : ScriptableObject
{
    public abstract bool IsValid(Agent.IAgent agent);
}