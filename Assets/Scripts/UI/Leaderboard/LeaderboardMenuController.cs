using System.Collections;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class LeaderboardMenuController : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardElement;
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _content;

    public void Show()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            var child = _content.GetChild(i);
            Destroy(child.gameObject);
        }

        StartCoroutine(Get(10));
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }
    
    private IEnumerator Get(int n)
    {
        string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
        string contractAddress = "0x2e8c3a9cA1c80A7A84D5d474FB440F975D9C6e48";

        var queryRequest = new QueryUnityRequest<GetLeaderboardFunction, GetLeaderboardOutputDTO>(url, contractAddress);
        yield return queryRequest.Query(new GetLeaderboardFunction() { N = n }, contractAddress);
        var userList = queryRequest.Result.ReturnValue1;

        foreach (var user in userList)
        {
            var child = Instantiate(leaderboardElement, _content);
            var element = child.GetComponent<LeaderboardUIElement>();
            element.UpdateUI(user.User,(int)user.Score);
        }
        
        _panel.SetActive(true);
    }
}