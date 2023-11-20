using System;
using UnityEngine;


[CreateAssetMenu(menuName = "GOAP/Actions/Action2", fileName ="New Action2", order = 1)]
public class Action : ScriptableObject
{
    public Prerequisite[] Prerequisites => _prerequisites;
    [SerializeField] private Prerequisite[] _prerequisites = { };

    public Effect[] Effects => _effects;
    [SerializeField] private Effect[] _effects = { };

    public virtual Vector3 Position
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

public class GameplayAction : Action
{
    [SerializeField]
    private Type _interactable;

    public override Vector3 Position => _interactable.Position;
}