using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour//Levels menu controller
{
    [SerializeField] private GameObject _panel;//level menu panel
    
    [SerializeField] private GameObject _prefab;//each leve menu element prefab
    [SerializeField] private Transform _content;//level menu content panel
    
    public void Show()//Show level menu
    {
        int levelCount = SceneManager.sceneCountInBuildSettings - 1;//get total level count

        var level = GameManager.Instance.userData.level;//get user progress data
        
        for (int i = 0; i < levelCount; i++)//create level elements and set texts of element
        {
            var child = Instantiate(_prefab, _content);
            var element = child.GetComponent<LevelUIElement>();
            element.UpdateUI(i<level,i+1);
        }
        
        _panel.SetActive(true);//Set visible panel
    }

    public void Hide()//Hide levels panel
    {
        _panel.SetActive(false);
    }
}