using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

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
        GameplayAction[] _actions;

        private string[] _goalTypes = new[] { "Have Item" };

        private Item _item;
        private int _itemCount;
        
        
        private PrerequisiteType _currentGoalType;

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

            int index = 0;
            foreach (GameplayAction action in _actions)
            {
                EditorGUILayout.ObjectField("Action " + (index + 1), action, typeof(GameplayAction), true);
                index++;
            }

            int selected = (int) _currentGoalType;
            selected = EditorGUILayout.Popup("Goal", selected, _goalTypes);
            _currentGoalType = (PrerequisiteType)selected;
            
            GUILayout.BeginVertical("Goal Parameters", "window");
            
            switch (_currentGoalType)
            {
                case PrerequisiteType.HaveItem:
                    _item = EditorGUILayout.ObjectField("Item", _item, typeof(Item)) as Item;
                    _itemCount = EditorGUILayout.IntField("Item Count", _itemCount);
                    break;

                default:
                    break;
            }
            
            GUILayout.EndVertical();

            if (GUILayout.Button("Set Goal"))
            {
                switch (_currentGoalType)
                {
                    case PrerequisiteType.HaveItem:
                        if (_item is null)
                        {
                            return;
                        }
                        _currentGoal = new Goal();
                        _currentGoal.Required = new ItemOwnedPrecondition(_item, _itemCount);
                        agentBehaviour.PlanActions(_currentGoal);
                        break;
                    
                    default:
                        break;
                }
            }
            
            SaveChanges();

        }
    }
}