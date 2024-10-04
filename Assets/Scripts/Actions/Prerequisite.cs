using System;
using Unity.VisualScripting;
using UnityEngine;

public enum PrerequisiteType
{
    HaveItem,
    NotHaveItem
}

[System.Serializable]
public class Prerequisite : ScriptableObject
{
    public PrerequisiteType Type;
    
    public virtual bool IsValid(Agent.IAgent agent)
    {
        return true;
    }
}