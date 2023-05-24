using System;
using UnityEngine;

[System.Serializable]
public class MainMenuPanel//Login and Register panel class
{
    public GameObject panel;//login or register panel
    public TMPro.TMP_InputField nickname;//nickname text
    public TMPro.TMP_InputField password;//password text
    public TMPro.TextMeshProUGUI warning;//warning text

    public void Show()//Show panel
    {
        panel.SetActive(true);//set panel visible
        
        ClearInputs();//clear inputs
    }

    public void ShowWarning(string text)//show warning
    {
        warning.text = text;//Set warning text
    }

    public void Hide()//Hide panel
    {
        panel.SetActive(false);//set panel invisible
        
        ClearInputs();//clear inputs
    }

    private void ClearInputs()//Clear inputs
    {
        nickname.text = String.Empty;
        password.text = String.Empty;
        warning.text = String.Empty;
    }
}