using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    
    [SerializeField] private MainMenuPanel _login;
    [SerializeField] private MainMenuPanel _register;

    [SerializeField] private NetworkManager _networkManager;

    public void ShowLogin()//Show Login page
    {
        _login.Show();
        _register.Hide();
    }

    public void ShowRegister()
    {
        _register.Show();
        _login.Hide();
    }

    public void Login()
    {
        var nickname = _login.nickname.text;
        var password = _login.password.text;
        
        if (nickname == String.Empty || password == String.Empty)
        {
            string warning = "Nickname or Password can not be empty";
            _login.ShowWarning(warning);
            return;
        }
        
        _networkManager.Login(nickname,password,() =>
        {
            Hide();
        });

    }

    public void Register()
    {
        var nickname = _register.nickname.text;
        var password = _register.password.text;
        
        if (nickname == String.Empty || password == String.Empty)
        {
            string warning = "Nickname or Password can not be empty";
            _login.ShowWarning(warning);
            return;
        }
        
        _networkManager.Register(nickname,password,() =>
        {
            Hide();
        });
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Hide()
    {
        _panel.SetActive(false);
    }
}