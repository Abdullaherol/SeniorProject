using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor: Editor//Game Manager Editor Script for custom inspector
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset"))//Add button to inspector
        {
            PlayerPrefs.DeleteAll();//Delete save
        }
    }
}