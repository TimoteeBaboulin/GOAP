using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Prerequisite), true)]
public class PrerequisiteCustomDrawer : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);
     
        // Draw foldout arrow
        if (property.objectReferenceValue != null)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        
        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            //EditorGUI.indentLevel++;
         
            //GUILayout.BeginHorizontal();

            //EditorGUILayout.PropertyField(property.FindPropertyRelative("Item"));
            //EditorGUILayout.PropertyField(property.FindPropertyRelative("Amount"));
            
            // Draw object properties
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
            editor.OnInspectorGUI();
         
            // Set indent back to what it was
            //EditorGUI.indentLevel--;
            
            //GUILayout.EndHorizontal();
        }
        
        
    }
}