using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;

public class SaveTutorialState : MonoBehaviour
{
    void Start()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = null
        }, result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("tutorial")) Debug.Log("No tutorial data");
            else {
                Debug.Log("tutorial: " + result.Data["tutorial"].Value);
                if (result.Data["tutorial"].Value == "No") { }
                {
                    gameObject.SetActive(false);
                }
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });

    }

    void Update()
    {
        
    }

    public void SetTutorialState(string state)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"tutorial", state},
        }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
