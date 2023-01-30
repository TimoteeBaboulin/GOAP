using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Goal", fileName = "New Goal", order = 0)]
public class Goal : Action{
    public Effect Prerequisite => _prerequisite;
    [SerializeField] private Effect _prerequisite;
}