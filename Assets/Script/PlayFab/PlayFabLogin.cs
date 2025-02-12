using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class InitialUsersList
{
    public List<User> Users = new List<User>();
}
[Serializable]
public class User
{
    public string UserName;
    public string Password;
}
[Serializable]
public class UserInizalized
{
    public bool isInizalized;
}
[SerializeField]
public class PlayerValues
{
    public string UserName;
    public string Password;
}
public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text incorrectUserText;
    [SerializeField] private TMP_InputField playerPasswordInput;
    [SerializeField] private TMP_InputField playerNameInput;

    [Header("Event")]
    [SerializeField] private UnityEvent enterSimulator;
    private string playerName;
    private string playerPassword;

    private bool isLoggedIn;
    public void Start()
    {
       isLoggedIn = false;
    }
    private void OnLoginSuccess(LoginResult result)
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = playerNameInput.text;
        request.Password = playerPasswordInput.text;
        request.TitleId = PlayFabSettings.staticSettings.TitleId;
        PlayFabClientAPI.LoginWithPlayFab(request,LoginCallback,OnLoginFailure);
        
    }
    private void LoginCallback(LoginResult result)
    {
        enterSimulator.Invoke();
        print(result);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        incorrectUserText.text = "Incorrect User or Password";
        playerNameInput.text = string.Empty;
        playerPasswordInput.text = string.Empty;
        StartCoroutine(DeleteText());
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void PressedLoading()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "6CC33";
        }
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = playerNameInput.text;
        request.Password = playerPasswordInput.text;
        request.TitleId = PlayFabSettings.staticSettings.TitleId;
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }
    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(3f);
        incorrectUserText.text = string.Empty;
    }
}