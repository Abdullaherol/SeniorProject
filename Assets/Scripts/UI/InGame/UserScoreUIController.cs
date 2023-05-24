using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine.SceneManagement;

public class UserScoreUIController : MonoBehaviour//Write user score to user screen.
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreTxt;//score text
    private int _lastScore;//last score 

    private void Start()//Start Function
    {
        _scoreTxt.gameObject.SetActive(false);
        UIManager.Instance.OnWordCompleted += InstanceOnOnWordCompleted;
        UpdateScore();
    }

    private void InstanceOnOnWordCompleted(WordSaveData wordsavedata)//On word completed
    {
        if (_lastScore != 0)//check last score is not 0
        {
            _lastScore += wordsavedata.CalculatePoint();//increase score
            _scoreTxt.text = "Total Score: "+ _lastScore;//update score text
        }
    }

    public void ExitLevel()//Exit Level button function
    {
        StopAllCoroutines();//stop all coroutines
        SceneManager.LoadScene(0);//load main level
    }

    private void UpdateScore()//update score
    {
        StartCoroutine(Get());//start coroutine to get score data from blockchain
    }
    
    private IEnumerator Get()//Get score data from blockchain
    {
        yield return new WaitForSeconds(1f);
        
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0x2e8c3a9cA1c80A7A84D5d474FB440F975D9C6e48";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = 250 }, contractAddress);
        var hasPoint = queryRequest.Result.ReturnValue1.Any(x=>x.User == GameManager.Instance.userData.nickname);//Get first 250 users from leaderboard

        if (hasPoint)//if leaderboard contains user
        {
            var user = queryRequest.Result.ReturnValue1.First(x => x.User == GameManager.Instance.userData.nickname);//get user data
        
            _scoreTxt.gameObject.SetActive(true);//set visible user score text
            
            _scoreTxt.text = "Total Score: "+ user.Score;//update user score text 

            _lastScore = (int)user.Score;//set score
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();//stop all coroutines
    }
}