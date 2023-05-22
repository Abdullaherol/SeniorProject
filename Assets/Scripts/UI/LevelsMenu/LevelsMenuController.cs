using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _content;
    
    public void Show()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings - 1;

        var level = GameManager.Instance.userData.level;
        
        for (int i = 0; i < levelCount; i++)
        {
            var child = Instantiate(_prefab, _content);
            var element = child.GetComponent<LevelUIElement>();
            element.UpdateUI(i<level,i+1);
        }
        
        _panel.SetActive(true);
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }
}