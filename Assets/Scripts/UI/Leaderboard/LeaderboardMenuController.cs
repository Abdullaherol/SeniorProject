using System.Collections;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class LeaderboardMenuController : MonoBehaviour//Leaderboard menu controller
{
    [SerializeField] private GameObject leaderboardElement;//each sub element prefab
    
    [SerializeField] private GameObject _panel;//leaderboard panel
    [SerializeField] private Transform _content;//leaderboard content panel

    public void Show()//Show leaderboard panel
    {
        for (int i = 0; i < _content.childCount; i++)//First destroy previous element in content panel
        {
            var child = _content.GetChild(i);
            Destroy(child.gameObject);
        }

        StartCoroutine(Get(10));//start coroutine for get top 10 leaderboard data
    }

    public void Hide()//Hide leaderboard panel
    {
        _panel.SetActive(false);
    }
    
    private IEnumerator Get(int n)//Get leaderboard data from blockchain
    {
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0x2e8c3a9cA1c80A7A84D5d474FB440F975D9C6e48";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = n }, contractAddress);
        var userList = queryRequest.Result.ReturnValue1;

        foreach (var user in userList)//get users from blockchain 
        {
            var child = Instantiate(leaderboardElement, _content);//create element into content
            var element = child.GetComponent<LeaderboardUIElement>();//get leaderboarduielement component from element
            element.UpdateUI(user.User,(int)user.Score);//update element data from user data
        }
        
        _panel.SetActive(true);//set visible leaderboard panel
    }
}