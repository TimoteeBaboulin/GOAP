using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrerequisiteCreationEditorWindow : EditorWindow
{
    private SerializedProperty _prerequisiteArray;
    private Action<Prerequisite> OnCommit;

    private int _amount;
    private Item _item;

    private PrerequisiteType _currentType = PrerequisiteType.HaveItem;
    
    [MenuItem("Window/UI Toolkit/PrerequisiteEditorWindow")]
    public static PrerequisiteCreationEditorWindow ShowExample(Action<Prerequisite> callback)
    {
        if (callback == null)
        {
            return null;
        }
        PrerequisiteCreationEditorWindow wnd = GetWindow<PrerequisiteCreationEditorWindow>();
        wnd.titleContent = new GUIContent("Prerequisite Editor");
        wnd.OnCommit = callback;
        return wnd;
    }

    public void OnGUI()
    {
        _currentType = (PrerequisiteType) EditorGUILayout.EnumPopup("Type", _currentType);
        
        switch (_currentType)
        {
            case PrerequisiteType.HaveItem:
            case PrerequisiteType.NotHaveItem:
                _item = EditorGUILayout.ObjectField("Item", _item, typeof(Item)) as Item;
                _amount = EditorGUILayout.IntField("Amount", _amount);
                break;
        }
        
        if (GUILayout.Button("Commit"))
        {
            Prerequisite prerequisite;
            switch (_currentType)
            {
                case PrerequisiteType.HaveItem:
                    prerequisite = CreateInstance<ItemOwnedPrecondition>();
                    if (prerequisite is null) break;
                    (prerequisite as ItemOwnedPrecondition).Item = _item;
                    (prerequisite as ItemOwnedPrecondition).Amount = _amount;
                    prerequisite.name = "Have " + _amount + " " + _item.Name;
                    break;
                case PrerequisiteType.NotHaveItem:
                    prerequisite = CreateInstance<ItemNotOwnedPrecondition>();
                    if (prerequisite is null) break;
                    (prerequisite as ItemNotOwnedPrecondition).Item = _item;
                    (prerequisite as ItemNotOwnedPrecondition).Amount = _amount;
                    prerequisite.name = "Not Have " + _amount + " " + _item.Name;
                    break;
                default:
                    return;
            }
            
            OnCommit?.Invoke(prerequisite);
            this.Close();
        }
    }
}
