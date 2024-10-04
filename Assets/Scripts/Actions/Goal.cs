using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Actions/Goal2", fileName = "New Goal", order = 10)]
public class Goal : ScriptableObject
{
    

    
    public Prerequisite Required
    {
        get { return _goal; }
        set { _goal = value; }
    }

    private PrerequisiteType _type;
    
    [SerializeField] private Prerequisite _goal;
}
