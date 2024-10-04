using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace DefaultNamespace
{
    [CustomEditor(typeof(GameplayAction), true)]
    [CanEditMultipleObjects]
    public class GameplayActionEditor : Editor
    {
       
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            SerializedProperty prerequisitesProperty = serializedObject.FindProperty("_prerequisites");
            
            if (GUILayout.Button("Add Prerequisite"))
            { 
                var wnd = PrerequisiteCreationEditorWindow.ShowExample(AddPrerequisite);
            }

            if (GUILayout.Button("Edit Prerequisites"))
            {
                PrerequisiteEditEditorWindow.ShowExample(prerequisitesProperty);
            }
            
            serializedObject.ApplyModifiedProperties();
            return;
            
            /*
            for (int i = 0; i < gameplayAction.Prerequisites.Count; i++)
            {
                GUILayout.BeginHorizontal();
                
                PrerequisiteType currentType = gameplayAction.Prerequisites[i].Type;
                PrerequisiteType newType;
                
                newType = (PrerequisiteType) EditorGUILayout.EnumPopup(currentType);
                
                if (newType != currentType)
                {
                    switch (newType)
                    {
                        case PrerequisiteType.HaveItem:
                            gameplayAction.Prerequisites[i] = new ItemOwnedPrecondition();
                            break;
                        case PrerequisiteType.NotHaveItem:
                            gameplayAction.Prerequisites[i] = new ItemNotOwnedPrecondition();
                            break;
                        default:
                            break;
                    }
                }

                switch (newType)
                {
                    case PrerequisiteType.HaveItem:
                        ItemOwnedPrecondition itemOwnedPrecondition = (ItemOwnedPrecondition) gameplayAction.Prerequisites[i];
                        itemOwnedPrecondition.Item = (Item)EditorGUILayout.ObjectField(itemOwnedPrecondition.Item, typeof(Item));
                        itemOwnedPrecondition.Amount = EditorGUILayout.IntField(itemOwnedPrecondition.Amount);
                        break;
                    
                    case PrerequisiteType.NotHaveItem:
                        ItemNotOwnedPrecondition itemNotOwnedPrecondition = (ItemNotOwnedPrecondition) gameplayAction.Prerequisites[i];
                        itemNotOwnedPrecondition.Item = (Item)EditorGUILayout.ObjectField(itemNotOwnedPrecondition.Item, typeof(Item));
                        itemNotOwnedPrecondition.Amount = EditorGUILayout.IntField(itemNotOwnedPrecondition.Amount);
                        break;
                    
                    default:
                        break;
                }
                
                EditorGUILayout.PropertyField(gameplayAction.Prerequisites[i]);
                
                GUILayout.EndHorizontal();
            }
            */
            
            serializedObject.ApplyModifiedProperties();
        }

        public void AddPrerequisite(Prerequisite prerequisite)
        {
            SerializedProperty prerequisitesProperty = serializedObject.FindProperty("_prerequisites");
            prerequisitesProperty.arraySize++;

            SerializedProperty newPrerequisite = prerequisitesProperty.GetArrayElementAtIndex(prerequisitesProperty.arraySize - 1);
            newPrerequisite.boxedValue = prerequisite;
            serializedObject.ApplyModifiedProperties();
        }
        
    }
}