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

        Goal _currentGoal;
        Action[] _actions;

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

            EditorGUILayout.ObjectField("Goal", _currentGoal, typeof(Goal), true);
            int index = 0;
            foreach (Action action in _actions)
            {
                EditorGUILayout.ObjectField("Action " + (index + 1), action, typeof(Action), true);
                index++;
            }
            
        }
    }
}