using System;
using UnityEngine;

//Potentially useless
[Serializable]
public struct Effect{
    public string effectName;
    public bool value;
}

[Serializable]
[CreateAssetMenu(menuName = "GOAP/Action", fileName = "New Action", order = 0)]
public class Action : ScriptableObject{
    public Effect[] Prerequisites => _prerequisites;
    [SerializeField] private Effect[] _prerequisites;

    public Effect[] Effects => _effects;
    [SerializeField] private Effect[] _effects;

    public Vector3 Position{
        get{
            if (PositionsManager.GetPositionOfWaypoint(_waypoint, out var position))
                return position;
            throw new NullReferenceException("Couldn't find waypoint with name: " + _waypoint);
        }
    }
    [SerializeField] private string _waypoint;
    
    public int Cost => _cost;
    [SerializeField] private int _cost;
}

[CreateAssetMenu(menuName = "GOAP/Actions/Action2", fileName ="New Action2", order = 1)]
public class Action2 : ScriptableObject
{
    public Prerequisite[] Prerequisites => _prerequisites;
    [SerializeField] private Prerequisite[] _prerequisites = { };

    public Effect2[] Effects => _effects;
    [SerializeField] private Effect2[] _effects = { };

    public Vector3 Position
    {
        get
        {
            if (PositionsManager.GetPositionOfWaypoint(_waypoint, out var position))
                return position;
            throw new NullReferenceException("Couldn't find waypoint with name: " + _waypoint);
        }
    }
    [SerializeField] private string _waypoint;

    public int Cost => _cost;
    [SerializeField] private int _cost;
}