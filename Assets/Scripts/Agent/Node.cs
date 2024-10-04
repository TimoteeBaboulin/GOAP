using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Node
{
    public readonly GameplayAction Action;
    public readonly Goal Goal;
    public float GetCost(Vector3 _position)
    {
        return Action != null ? Action.GetCost(_position) : 0;
    }

    public float value;

    public List<Node> Children = new();
    public List<Node> Parent = new();

    public bool isGoal;
    public Vector3 Position => Action.Position;

    public Node(GameplayAction action)
    {
        Action = action;
        isGoal = false;
    }

    public Node(Goal goal)
    {
        Goal = goal;
        isGoal = true;
    }
}