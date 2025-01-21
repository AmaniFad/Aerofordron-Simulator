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
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { "IsInizalized" } },
           dataResult =>
        {
           if (dataResult.Data.ContainsKey("IsInizalized") == false)
           {
                InizalizeUser();
           }
           else
           {
                comprobeUserLogin();
           }
        }, error => {});
    }
    private void comprobeUserLogin()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { "PlayerValues" } },
           dataResult =>
           {
               if (dataResult.Data.ContainsKey("PlayerValues"))
               {
                   var playerValuesJson = dataResult.Data["PlayerValues"].Value;
                   var playerValuesList = JsonUtility.FromJson<PlayerValues>(playerValuesJson);
                   print(playerValuesList.UserName + " Pass: " + playerValuesList.Password);
                   if (playerValuesList.UserName.Equals(playerName) && playerValuesList.Password.Equals(playerPassword))
                   {
                       enterSimulator.Invoke();
                   }
                   else
                   {
                       incorrectUserText.text = "Incorrect User or Password";
                       playerNameInput.text = string.Empty;
                       playerPasswordInput.text = string.Empty;
                       StartCoroutine(DeleteText());
                   }
               }
               else
               {
                   Debug.LogWarning("No se encontraron los datos del jugador.");
                   incorrectUserText.text = "User data not found";
                   StartCoroutine(DeleteText());
               }

           }, error => { });
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

                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                    {
                        Data = new Dictionary<string, string>
                        {
                            {
                                "IsInizalized", 
                                JsonUtility.ToJson(new UserInizalized{isInizalized = true})
                            },
                            { 
                                "PlayerValues",
                                JsonUtility.ToJson(new PlayerValues{UserName = playerName, Password = playerPassword})
                            },
                        },

                    }, result => { }, error => { });
                    enterSimulator.Invoke();
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
        }
    }
    public void GetPassword()
    {
        if (playerPasswordInput != null)
        {
            playerPassword = playerPasswordInput.text;
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
        var request = new LoginWithCustomIDRequest { CustomId = playerName, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(3f);
        incorrectUserText.text = string.Empty;
    }
}