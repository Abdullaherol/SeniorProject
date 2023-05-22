using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Network;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public void Login(string nickname,string password,Action action)
    {
        StartCoroutine(LoginRequest(nickname,password,action));
    }

    public void Register(string nickname,string password,Action action)
    {
        StartCoroutine(RegisterRequest(nickname,password,action));
    }

    public void SaveProgress(int level, Action action)
    {
        var username = GameManager.Instance.userData.nickname;
        
        StartCoroutine(SaveProgressRequest(username, level, action));
    }

    private IEnumerator SaveProgressRequest(string username, int level, Action action)
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorSaveProgress";
        
        WWWForm form = new WWWForm();
        form.AddField("Nickname",username);
        form.AddField("Level_Id",level);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                action.Invoke();
                GameManager.Instance.userData.level = level;
            }
        
            request.Dispose();
        };
    }

    IEnumerator LoginRequest(string nickname,string password,Action action)
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorLogin";
        
        WWWForm form = new WWWForm();
        form.AddField("Nickname",nickname);
        form.AddField("Password",password);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var data = JsonConvert.DeserializeObject<LoginResponseData>(request.downloadHandler.text);
                action.Invoke();
            
                GameManager.Instance.userData = new UserData()
                {
                    nickname = nickname,
                    level = data.levelNumber
                };
            }
        
            request.Dispose();
        };
        
    }
    
    IEnumerator RegisterRequest(string nickname,string password,Action action)
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorRegister";
        
        WWWForm form = new WWWForm();
        form.AddField("Nickname",nickname);
        form.AddField("Password",password);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                action.Invoke();

                GameManager.Instance.userData = new UserData()
                {
                    nickname = nickname,
                    level = 1
                };
            }
        
            request.Dispose();
        }
        
    }
}