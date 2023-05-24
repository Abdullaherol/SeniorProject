using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour//Main menu controller
{
    [SerializeField] private GameObject _panel;//main menu panel
    
    [SerializeField] private MainMenuPanel _login;//login panel
    [SerializeField] private MainMenuPanel _register;//register panel

    [SerializeField] private NetworkManager _networkManager;//networkmanager

    public void ShowLogin()//Show Login page
    {
        _login.Show();//show login panel
        _register.Hide();//hide register panel
    }

    public void ShowRegister()//Show register panel
    {
        _register.Show();//show register panel
        _login.Hide();//hide login panel
    }

    public void Login()//Login process
    {
        var nickname = _login.nickname.text;//get nickname from user input
        var password = _login.password.text;//get password from user input
        
        if (nickname == String.Empty || password == String.Empty)//check nickname and password
        {
            string warning = "Nickname or Password can not be empty";//Show warning to user if nickname or password is empty
            _login.ShowWarning(warning);
            return;
        }
        
        _networkManager.Login(nickname,password,() =>//send login request
        {
            Hide();//when receive response from login request, hide login panel 
        });

    }

    public void Register()//Register process
    {
        var nickname = _register.nickname.text;//get nickname from user input
        var password = _login.password.text;//get password from user input
        
        if (nickname == String.Empty || password == String.Empty)//check nickname and password
        {
            string warning = "Nickname or Password can not be empty";//Show warning to user if nickname or password is empty
            _login.ShowWarning(warning);
            return;
        }
        
        _networkManager.Register(nickname,password,() =>//send register request
        {
            Hide();//when receive response from login request, hide register panel 
        });
    }

    public void ExitGame()//Exit game button function
    {
        Application.Quit();//close application
    }

    public void Hide()//hide panels
    {
        _panel.SetActive(false);
    }
}