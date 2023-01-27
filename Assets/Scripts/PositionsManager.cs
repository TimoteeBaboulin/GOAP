using System;
using UnityEngine;

[Serializable]
public struct Waypoints{
    public string name;
    public Transform transform;
}

public class PositionsManager : MonoBehaviour{
    public static PositionsManager Instance;
    public Waypoints[] Waypoints; 

    private void Start(){
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    public static bool GetPositionOfWaypoint(string waypoint, out Vector3 position){
        for (int x = 0; x < Instance.Waypoints.Length; x++){
            if (Instance.Waypoints[x].name == waypoint){
                position = Instance.Waypoints[x].transform.position;
                return true;
            }
        }

        position = Vector3.zero;
        return false;
    }
}