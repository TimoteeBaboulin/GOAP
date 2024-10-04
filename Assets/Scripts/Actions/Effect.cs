using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public abstract class Effect : ScriptableObject
{
    public abstract bool Activate(Agent.IAgent agent);
}