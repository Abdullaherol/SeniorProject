using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIElement : MonoBehaviour
{
    public int levelIndex;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private bool _canPlay;

    public void UpdateUI(bool canPlay, int index)
    {
        levelIndex = index;
        _canPlay = canPlay;

        _text.text = "Level " + levelIndex;
        
        _image.gameObject.SetActive(!_canPlay);
    }

    public void GoLevel()
    {
        if (_canPlay)
            SceneManager.LoadScene(levelIndex);
    }
}