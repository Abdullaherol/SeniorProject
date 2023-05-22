using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userData;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance.userData.nickname != string.Empty)
                FindObjectOfType<MainMenuController>().Hide();

            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Instance = this;
    }
}