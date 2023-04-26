using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData userData;

    private void Awake()
    {
        Instance = this;

        userData = new UserData()
        {
            level = 1,
            nickname = "Apo"
        };
    }
}