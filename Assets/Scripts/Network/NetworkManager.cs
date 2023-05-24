using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Network;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour//Control rest api request
{
    public void Login(string nickname,string password,Action action)//Login function
    {
        StartCoroutine(LoginRequest(nickname,password,action));//Start coroutine for login request
    }

    public void Register(string nickname,string password,Action action)//Register Function
    {
        StartCoroutine(RegisterRequest(nickname,password,action));//Start coroutine for register request
    }

    public void SaveProgress(int level, Action action)//SaveProgress Function
    {
        var username = GameManager.Instance.userData.nickname;//get username
        
        StartCoroutine(SaveProgressRequest(username, level, action));//start coroutine for saveProgress request
    }

    private IEnumerator SaveProgressRequest(string username, int level, Action action)//Request of SaveProgress 
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorSaveProgress";//request url
        
        WWWForm form = new WWWForm();//www form for post data
        form.AddField("Nickname",username);//add username to post data
        form.AddField("Level_Id",level);//add progress data to post data

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))//create web request
        {
            request.downloadHandler = new DownloadHandlerBuffer();//create download Handler
        
            yield return request.SendWebRequest();//send web request

            if (request.result == UnityWebRequest.Result.Success)//Check Response
            {
                action.Invoke();//execute action
                GameManager.Instance.userData.level = level;//set user level data from response data
            }
        
            request.Dispose();//and dispose request
        };
    }

    IEnumerator LoginRequest(string nickname,string password,Action action)
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorLogin";//request url
        
        WWWForm form = new WWWForm();//www form for post data
        form.AddField("Nickname",nickname);//add username to post data
        form.AddField("Password",password);//add password to post data

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))//create web request
        {
            request.downloadHandler = new DownloadHandlerBuffer();//create download Handler
        
            yield return request.SendWebRequest();//send web request

            if (request.result == UnityWebRequest.Result.Success)//Check Response
            {
                var data = JsonConvert.DeserializeObject<LoginResponseData>(request.downloadHandler.text);//convert json data to response data
                action.Invoke();//execute action
            
                GameManager.Instance.userData = new UserData()
                {
                    nickname = nickname,
                    level = data.levelNumber
                };
            }
        
            request.Dispose();//and dispose request
        };
        
    }
    
    IEnumerator RegisterRequest(string nickname,string password,Action action)
    {
        var url = "https://kcdyn1k6dl.execute-api.us-east-1.amazonaws.com/SeniorRegister";
        
        WWWForm form = new WWWForm();//www form for post data
        form.AddField("Nickname",nickname);//add username to post data
        form.AddField("Password",password);//add password to post data

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))//create web request
        {
            request.downloadHandler = new DownloadHandlerBuffer();//create download Handler
        
            yield return request.SendWebRequest();//send web request

            if (request.result == UnityWebRequest.Result.Success)//Check Response
            {
                action.Invoke();//execute action

                GameManager.Instance.userData = new UserData()
                {
                    nickname = nickname,
                    level = 1
                };
            }
        
            request.Dispose();//and dispose request
        }
        
    }
}