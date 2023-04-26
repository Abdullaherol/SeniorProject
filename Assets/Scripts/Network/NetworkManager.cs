using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    
    [SerializeField] private string nickname;
    [SerializeField] private string password;

    public delegate EventHandler OnUserLoginHandler();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Login();
        }
    }

    public void Login()
    {
        StartCoroutine(LoginRequest());
    }

    // public void Register()
    // {
    //     StartCoroutine(RegisterRequest());
    // }

    // public bool IsValidNickname()
    // {
    //     
    // }
    //
    // IEnumerator IsValidNicknameRequest()
    // {
    //     
    // }
    //
    // IEnumerator RegisterRequest()
    // {
    //     
    // }

    IEnumerator LoginRequest()
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorLogin";
        
        WWWForm form = new WWWForm();
        form.AddField("Nickname",nickname);
        form.AddField("Password",password);
        
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.downloadHandler = new DownloadHandlerBuffer();
        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST request sent successfully.");
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("POST request failed. Error: " + request.error);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}