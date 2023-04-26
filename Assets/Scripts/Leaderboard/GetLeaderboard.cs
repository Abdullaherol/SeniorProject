using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class GetLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;
    
    void Start()
    {
        StartCoroutine(Get(100));
    }

    public void GetLeaderboardTopTen()
    {
        StartCoroutine(Get(10));
    }

    private IEnumerator Get(int n)
    {
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0x2e8c3a9cA1c80A7A84D5d474FB440F975D9C6e48";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = n }, contractAddress);
        var userList = queryRequest.Result.ReturnValue1;
        
        
        
    }
}