using UnityEngine;

public class LeaderboardUIElement : MonoBehaviour//Each leaderboard element class
{
    [SerializeField] private TMPro.TextMeshProUGUI nicknameTxt;//nickname text
    [SerializeField] private TMPro.TextMeshProUGUI pointTxt;//point text

    public void UpdateUI(string nickname, int point)//Update leaderboard element 
    {
        nicknameTxt.text = nickname;//set nickname text
        pointTxt.text = point.ToString();//set point text
    }
}