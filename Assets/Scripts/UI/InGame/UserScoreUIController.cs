using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using UnityEngine.SceneManagement;

public class UserScoreUIController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreTxt;
    private int _lastScore;

    private void Start()
    {
        _scoreTxt.gameObject.SetActive(false);
        UIManager.Instance.OnWordCompleted += InstanceOnOnWordCompleted;
        UpdateScore();
    }

    private void InstanceOnOnWordCompleted(WordSaveData wordsavedata)
    {
        if (_lastScore != 0)
        {
            _lastScore += wordsavedata.CalculatePoint();
            _scoreTxt.text = "Total Score: "+ _lastScore;
        }
        // UpdateScore();
    }

    public void ExitLevel()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(0);
    }

    private void UpdateScore()
    {
        StartCoroutine(Get());
    }
    
    private IEnumerator Get()
    {
        yield return new WaitForSeconds(1f);
        
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0x2e8c3a9cA1c80A7A84D5d474FB440F975D9C6e48";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = 250 }, contractAddress);
        var hasPoint = queryRequest.Result.ReturnValue1.Any(x=>x.User == GameManager.Instance.userData.nickname);

        if (hasPoint)
        {
            var user = queryRequest.Result.ReturnValue1.First(x => x.User == GameManager.Instance.userData.nickname);
        
            _scoreTxt.gameObject.SetActive(true);
            
            _scoreTxt.text = "Total Score: "+ user.Score;

            _lastScore = (int)user.Score;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}