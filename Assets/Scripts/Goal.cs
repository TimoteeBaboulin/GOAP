using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Goal", fileName = "New Goal", order = 0)]
public class Goal : Action{
    public Effect Prerequisite => _prerequisite;
    [SerializeField] private Effect _prerequisite;
}

[CreateAssetMenu(menuName = "GOAP/Actions/Goal2", fileName = "New Goal", order = 10)]
public class Goal2 : Action2
{
    public Prerequisite Goal => _goal;
    [SerializeField] private Prerequisite _goal;
}