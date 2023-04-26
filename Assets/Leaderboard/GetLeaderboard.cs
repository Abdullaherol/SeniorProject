using System.Collections;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using Leaderboard.LeaderboardContract;

public class GetLeaderboard : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Get(100));
    }

    private IEnumerator Get(int n)
    {
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0xFb711b3557415fE5Ab5111C59d5a57EBAcA1BFF2";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = n }, contractAddress);
        var userList = queryRequest.Result.UserList;
    }
}