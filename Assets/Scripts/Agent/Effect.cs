using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public abstract class Effect : ScriptableObject
{
    public abstract void Activate(Agent.IAgent agent);
}