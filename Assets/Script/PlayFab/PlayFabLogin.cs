using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
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
public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text incorrectUserText;
    [SerializeField] private TMP_InputField playerPasswordInput;
    [SerializeField] private TMP_InputField playerNameInput;
    private string playerName;
    private string playerPassword;

    private bool isLoggedIn;
    public void Start()
    {
       isLoggedIn = false;
    }

    private void OnLoginSuccess(LoginResult result)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { "IsInitialized" } },
           dataResult =>
        {
            if (!dataResult.Data.ContainsKey("IsInitialized"))
            {
                InizalizeUser();
            }
            else
            {
                string userName = dataResult.Data["username"].Value;
                string userPassword = dataResult.Data["password"].Value;

                if(userName.Equals(playerName) && userPassword.Equals(playerPassword))
                {
                    Debug.Log("entrando als simulador mas veces");
                }
                else
                {
                    incorrectUserText.text = "Incorrect User or Password";
                    playerNameInput.text = string.Empty;
                    playerPasswordInput.text = string.Empty;
                    StartCoroutine(DeleteText());
                }
            }
        }, error => {});

       
        Debug.Log("Congratulations, you made your first successful API call!");
    }
    private void InizalizeUser()
    {
        GetTitleDataRequest request = new GetTitleDataRequest
        {
            Keys = new List<string>() { "InitialUsersList" }
        };
        PlayFabClientAPI.GetTitleData(request, dataResult =>
        {
            var data = dataResult.Data["InitialUsersList"];
            var initialusersList = JsonUtility.FromJson<InitialUsersList>(data);

            if (playerName != null && playerPassword != null)
            {
                isLoggedIn = false;
                for (int i = 0; i < initialusersList.Users.Count; i++)
                {
                    if (initialusersList.Users[i].UserName.Equals(playerName)
                    && initialusersList.Users[i].Password.Equals(playerPassword))
                    {
                        isLoggedIn = true;
                    }
                }
                if (isLoggedIn)
                {
                    Debug.Log("entrando als simulador primera vez");

                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                    {
                        Data = new Dictionary<string, string>
                        {
                            {
                                "IsInizalized", 
                                JsonUtility.ToJson(new UserInizalized{ isInizalized = true})
                            },
                            { 
                                "username",
                                JsonUtility.ToJson(playerName)
                            },
                            {
                                "password",
                                JsonUtility.ToJson(playerPassword)
                            }
                        },

                    }, result => { }, error => { });
                }
                else
                {
                    incorrectUserText.text = "Incorrect User or Password";
                    playerNameInput.text = string.Empty;
                    playerPasswordInput.text = string.Empty;
                    StartCoroutine(DeleteText());
                }
            }
        }, error => { });
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetName()
    {
        if(playerNameInput != null)
        {
            playerName = playerNameInput.text;
            Debug.Log("player name" + playerName);
        }
    }
    public void GetPassword()
    {
        if (playerPasswordInput != null)
        {
            playerPassword = playerPasswordInput.text;
            Debug.Log(" player password" + playerPassword);
        }
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
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(3f);
        incorrectUserText.text = string.Empty;
    }
}