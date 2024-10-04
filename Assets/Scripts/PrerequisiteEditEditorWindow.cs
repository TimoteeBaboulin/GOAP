using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrerequisiteEditEditorWindow : EditorWindow
{
    private SerializedProperty _prerequisiteArray;
    private Action<Prerequisite> OnCommit;

    private int _amount;
    private Item _item;

    private int _index;
    
    private PrerequisiteType _currentType = PrerequisiteType.HaveItem;
    
    [MenuItem("Window/UI Toolkit/PrerequisiteEditorWindow")]
    public static PrerequisiteEditEditorWindow ShowExample(SerializedProperty array)
    {
        PrerequisiteEditEditorWindow wnd = GetWindow<PrerequisiteEditEditorWindow>();
        wnd._prerequisiteArray = array;
        wnd.titleContent = new GUIContent("Prerequisite Editor");
        return wnd;
    }

    public void OnGUI()
    {
        if (_prerequisiteArray is null)
        {
            Debug.Log("Prerequisite Array is null");
            return;
        }
        
        Rect rect = this.rootVisualElement.contentRect;
        float width = rect.width;
        float height = rect.height;
        List<string> names = new List<string>();
        for (int i = 0; i < _prerequisiteArray.arraySize; i++)
        {
            SerializedProperty element = _prerequisiteArray.GetArrayElementAtIndex(i);
            Prerequisite prerequisite = element.objectReferenceValue as Prerequisite;
            switch (prerequisite.Type)
            {
                case PrerequisiteType.HaveItem:
                    names.Add(i + " - Have Item " + (prerequisite as ItemOwnedPrecondition).Amount + " " + (prerequisite as ItemOwnedPrecondition).Item.Name);
                    break;
                case PrerequisiteType.NotHaveItem:
                    names.Add(i + " - Not Have Item " + (prerequisite as ItemNotOwnedPrecondition).Amount + " " + (prerequisite as ItemNotOwnedPrecondition).Item.Name);
                    break;  
            }
        }
        
        using (var areaScope = new GUILayout.AreaScope(new Rect(rect.xMin + 10, rect.yMin, width * 0.4f - 5, height)));
        {
            _index = EditorGUILayout.Popup("Prerequisites", _index, names.ToArray(), GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
        }
        
        if (_prerequisiteArray.arraySize == 0 || _index >= _prerequisiteArray.arraySize)
        {
            return;
        }

        using (var areaScope =
               new GUILayout.AreaScope(new Rect(rect.xMin + 0.4f * width + 5, rect.yMin + 10, width * 0.6f - 15, height)))
        {

            SerializedProperty serializedPrerequisite = _prerequisiteArray.GetArrayElementAtIndex(_index);
            Prerequisite selectedPrerequisite = serializedPrerequisite.objectReferenceValue as Prerequisite;
            SerializedObject prerequisiteObject = new SerializedObject(selectedPrerequisite);
            SerializedProperty serializedItem = prerequisiteObject.FindProperty("Item");
            
            switch (selectedPrerequisite.Type)
            {
                case PrerequisiteType.HaveItem:
                case PrerequisiteType.NotHaveItem:
                    EditorGUILayout.PropertyField(prerequisiteObject.FindProperty("Item"));
                    EditorGUILayout.PropertyField(prerequisiteObject.FindProperty("Amount"));
                    prerequisiteObject.ApplyModifiedProperties();
                    break;
            }

            

            if (GUILayout.Button("Remove Prerequisite"))
            {
                _prerequisiteArray.DeleteArrayElementAtIndex(_index);
            }
            
            _prerequisiteArray.serializedObject.ApplyModifiedProperties();
        }
    }
}