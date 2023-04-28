using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class GetUser : MonoBehaviour
{

  void Start()
  {
    StartCoroutine(GetUserByNickname("gavin"));
  }


  private IEnumerator GetUserByNickname(string nickname)
  {
    string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
    string contractAddress = "0x0d4Cf3a543EdD70fE351220199eC8EE81c51C422";

    var queryRequest = new QueryUnityRequest<FindUserByNicknameFunction, FindUserByNicknameOutputDTO>(url, contractAddress);
    yield return queryRequest.Query(new FindUserByNicknameFunction() { Nickname = nickname }, contractAddress);
    var userNickname = queryRequest.Result.ReturnValue1.User;
    var userScore = queryRequest.Result.ReturnValue1.Score;
  }
}