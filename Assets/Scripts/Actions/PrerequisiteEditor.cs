using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


    [CustomEditor(typeof(Prerequisite))]
    [CanEditMultipleObjects]
    public class PrerequisiteEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty type = serializedObject.FindProperty("Type");
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(type);
            if (EditorGUI.EndChangeCheck())
            {
                switch (type.enumValueIndex)
                {
                    case (int)PrerequisiteType.HaveItem:
                        serializedObject.targetObject.ConvertTo<ItemOwnedPrecondition>();
                        break;
                    case (int)PrerequisiteType.NotHaveItem:
                        serializedObject.targetObject.ConvertTo<ItemNotOwnedPrecondition>();
                        break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
