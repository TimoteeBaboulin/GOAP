using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Actions/Goal2", fileName = "New Goal", order = 10)]
public class Goal : Action
{
    public Prerequisite Required => _goal;
    [SerializeField] private Prerequisite _goal;
}
