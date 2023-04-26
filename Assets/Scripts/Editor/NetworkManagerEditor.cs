using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetworkManager))]
public class NetworkManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var networkManager = (NetworkManager)target;

        if (GUILayout.Button("Login"))
        {
            networkManager.Login();
        }
    }
}