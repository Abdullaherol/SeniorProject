using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIElement : MonoBehaviour//Each level element class
{
    public int levelIndex;//level index
    [SerializeField] private TMPro.TextMeshProUGUI _text;//level text
    [SerializeField] private Button _button;//level button
    [SerializeField] private Image _image;//level image

    private bool _canPlay;//can play variable

    public void UpdateUI(bool canPlay, int index)//Update level element 
    {
        levelIndex = index;
        _canPlay = canPlay;

        _text.text = "Level " + levelIndex;//set level text
        
        _image.gameObject.SetActive(!_canPlay);//show image if level is not playable
    }

    public void GoLevel()//If user click to element go this level
    {
        if (_canPlay)
            SceneManager.LoadScene(levelIndex);
    }
}