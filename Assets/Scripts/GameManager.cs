using System;
using UnityEngine;

public class GameManager : MonoBehaviour//Main control game class
{
    public static GameManager Instance;//Singleton

    public UserData userData;//user data

    private void Awake()//Awake Function for singleton
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