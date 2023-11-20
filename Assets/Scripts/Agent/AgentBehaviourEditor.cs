using System.Linq;
using UnityEditor;

namespace Agent
{
    
    [CustomEditor(typeof(AgentBehaviour))]
    [CanEditMultipleObjects]
    public class AgentBehaviourEditor : Editor
    {
        SerializedObject _agentBehaviour;
        SerializedProperty _agentInventory;
        SerializedProperty _agentGoal;
        SerializedProperty _agentActions;

        Goal2 _currentGoal;
        Action2[] _actions;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            AgentBehaviour agentBehaviour = ((AgentBehaviour)serializedObject.targetObject);

            _currentGoal = agentBehaviour.Agent.CurrentGoal;
            _actions = agentBehaviour.Agent.Actions.ToArray();

            for (int x = 0; x < agentBehaviour.Agent.Inventory.Inventory.Count; x++)
            {
                Item item = agentBehaviour.Agent.Inventory.Inventory.Keys.ToArray()[x];
                int amount = agentBehaviour.Agent.Inventory.Inventory[item];

                EditorGUILayout.IntField(item.name, amount);
            }

            EditorGUILayout.ObjectField("Goal", _currentGoal, typeof(Goal2), true);
            int index = 0;
            foreach (Action2 action in _actions)
            {
                EditorGUILayout.ObjectField("Action " + (index + 1), action, typeof(Action2), true);
                index++;
            }
            
        }
    }
}