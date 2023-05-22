using UnityEngine;

public class LeaderboardUIElement : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI nicknameTxt;
    [SerializeField] private TMPro.TextMeshProUGUI pointTxt;

    public void UpdateUI(string nickname, int point)
    {
        nicknameTxt.text = nickname;
        pointTxt.text = point.ToString();
    }
}