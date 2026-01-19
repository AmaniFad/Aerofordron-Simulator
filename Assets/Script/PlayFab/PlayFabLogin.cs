using JetBrains.Annotations;
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



public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text incorrectUserText;
    [SerializeField] private TMP_InputField playerPasswordInput;
    [SerializeField] private TMP_InputField playerNameInput;

    [Header("Event")]
    [SerializeField] private UnityEvent enterSimulator;
    bool onlyLogInOnce;
    public void Start()
    {
        DontDestroyOnLoad(this);
        onlyLogInOnce = true;
    }
    private void OnLoginSuccess(LoginResult result)
    {
        if (onlyLogInOnce)
        {
            GetUserDataRequest dataRequest = new GetUserDataRequest { PlayFabId = result.PlayFabId };

            PlayFabClientAPI.GetUserData(dataRequest,
             dataResult =>
             {
             string corporation = dataResult.Data["Corporation"].Value;
                 if (!string.IsNullOrEmpty(corporation))
                 {
                     CorporateLogosManager.instance.ChangeCorporation(corporation);
                 }
                 if (dataResult.Data["TimeLeft"].Value == "0")
                 {
                         onlyLogInOnce = false;
                     enterSimulator.Invoke();
                 }
                 else
                 {
                     
                     string format = "dd-MM-yyyy";
                     DateTime dt1 = DateTime.ParseExact(dataResult.Data["TimeLeft"].Value,format,null);
                     DateTime dt2 = DateTime.Now;
                     if (dt1 > dt2)
                     {
                         onlyLogInOnce = false;
                         enterSimulator.Invoke();
                     }

                     else
                     {
                         incorrectUserText.text = "Esta cuenta ya no es valida";
                         playerNameInput.text = string.Empty;
                         playerPasswordInput.text = string.Empty;
                         StartCoroutine(DeleteText());
                     }
                 }



                 Destroy(this);


             }, error => { print("Error en la peticion de datos"); });
            print(result);
        }


}


    private void OnLoginFailure(PlayFabError error)
    {
        incorrectUserText.text = error.ErrorMessage;
        playerNameInput.text = string.Empty;
        playerPasswordInput.text = string.Empty;
        StartCoroutine(DeleteText());
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void TryLogIn()
    {
        if (playerNameInput.text != "" && playerPasswordInput.text != "") 
        {
            if (onlyLogInOnce)
            {
                if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
                {
                    PlayFabSettings.staticSettings.TitleId = "6CC33";
                }
                LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
                request.Username = playerNameInput.text;
                request.Password = playerPasswordInput.text;
                request.TitleId = PlayFabSettings.staticSettings.TitleId;
                PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
            }
        }


    }
    private IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(3f);
        incorrectUserText.text = string.Empty;
    }
}