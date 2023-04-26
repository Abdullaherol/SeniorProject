using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuController : MonoBehaviour
{
    public static LevelsMenuController Instance;

    [SerializeField] private GameObject _prefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings - 1;
    }
}