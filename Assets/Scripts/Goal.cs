using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Goal", fileName = "New Goal", order = 0)]
public class Goal : ScriptableObject{
    public Action[] PossibleActions => _possibleActions;
    [SerializeField] private Action[] _possibleActions;
}