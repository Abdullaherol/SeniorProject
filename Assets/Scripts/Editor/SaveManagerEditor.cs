using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor//Save Manager Editor Script for custom inspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if(GUILayout.Button("Delete Save"))//Add button to inspector
        {
            PlayerPrefs.DeleteAll();//delete save
        }
    }
}