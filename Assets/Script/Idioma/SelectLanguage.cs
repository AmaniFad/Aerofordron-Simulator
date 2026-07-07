using UnityEngine;
using System.Collections;
using UnityEngine.Localization.Settings;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectLanguage : MonoBehaviour
{
    private bool active = false;
    private string startingScene;
    private void Start()
    {
        startingScene = SceneManager.GetActiveScene().name;
    }
    public void ChangeLocale(int localeID)
    {
        if (active== true)
          return;
        
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        if (PlayFabClientAPI.IsClientLoggedIn())
        SetLanguageOnServer(_localeID.ToString());
        active = false;
        if (SceneManager.GetActiveScene().name != startingScene)
        {
            Destroy(gameObject);
        }
    }

    public void SetLanguageOnServer(string language)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"language", language},
            }
        },
    result => Debug.Log("Successfully updated user data"),
    error =>
    {
        Debug.Log("Got error setting user data Ancestor to Arthur");
        Debug.Log(error.GenerateErrorReport());
    });
    }

    public void SetLanguageFromServer()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("language")) Debug.Log("No language data");
            else
            {
                Debug.Log("language: " + result.Data["language"].Value);
                ChangeLocale(int.Parse(result.Data["language"].Value));
    
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
