using System;
using UnityEngine;

[System.Serializable]
public class MainMenuPanel
{
    public GameObject panel;
    public TMPro.TMP_InputField nickname;
    public TMPro.TMP_InputField password;
    public TMPro.TextMeshProUGUI warning;

    public void Show()
    {
        panel.SetActive(true);
        
        ClearInputs();
    }

    public void ShowWarning(string text)
    {
        warning.text = text;
    }

    public void Hide()
    {
        panel.SetActive(false);
        
        ClearInputs();
    }

    private void ClearInputs()
    {
        nickname.text = String.Empty;
        password.text = String.Empty;
        warning.text = String.Empty;
    }
}