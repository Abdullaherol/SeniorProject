using System.Collections;
using Documents.Contracts.Leaderboard.ContractDefinition;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;

public class UpdateLeaderboard : MonoBehaviour
{
  void Start()
  {
    UIManager.Instance.OnWordCompleted += InstanceOnOnWordCompleted;
  }

  private void InstanceOnOnWordCompleted(WordSaveData wordsavedata)
  {
    var nickName = GameManager.Instance.userData.nickname;

    StartCoroutine(UpdateScore(nickName, wordsavedata.CalculatePoint()));
  }

  private IEnumerator UpdateScore(string user, int score)
  {
    string url = "https://sepolia.infura.io/v3/b1a52e3654bd480aa96cb8b4c0bce1dd";
    string privateKey = "27ccb36827fcff33b7b269455e5c1255e6cfdf79d5e887059a3e9601457949bf";
    string fromAddress = "0x5D0745e9b20107E9248162A163C08433070C8C9f";
    string contractAddress = "0x0d4Cf3a543EdD70fE351220199eC8EE81c51C422";

    var transactionTransferRequest = new TransactionSignedUnityRequest(url, privateKey);
    var transactionMessage = new AddScoreFunction
    {
      FromAddress = fromAddress,
      User = user,
      Score = score,
    };

    yield return transactionTransferRequest.SignAndSendTransaction(transactionMessage, contractAddress);
  }
}