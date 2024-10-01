using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

[Serializable]
public class ScoreValues
{
    public string SceneName;
    public int Score;
    public string ScoreTime;
}
public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
    } 

    public void ComprobeTitleData(string sceneName, int score, string scoreTime)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { sceneName } },
           dataResult =>
           {
               if (dataResult.Data.ContainsKey(sceneName) == false)
               {
                   SaveInfoPlayer(sceneName,score,scoreTime);
               }
               else
               {
                   UpdateInfoPlayer(sceneName, score, scoreTime);
               }
           }, error => { });
    }
    private void SaveInfoPlayer(string sceneName, int score, string scoreTime)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {
                    sceneName,
                    JsonUtility.ToJson(new ScoreValues{SceneName = sceneName, Score = score, ScoreTime = scoreTime})
                },
            },
        }, result => { }, error => { });
    }

    private void UpdateInfoPlayer(string sceneName, int score, string scoreTime)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { sceneName } },
            dataResult =>
            {
                if (dataResult.Data.TryGetValue(sceneName, out var playerValuesJson))
                {
                    var playerValuesList = JsonUtility.FromJson<ScoreValues>(playerValuesJson.Value);
                    playerValuesList.Score = score;
                    playerValuesList.ScoreTime = scoreTime;

                    var request = new UpdateUserDataRequest
                    {
                        Data = new Dictionary<string, string>
                        {
                        { sceneName, JsonUtility.ToJson(playerValuesList) }
                        }
                    };
                    PlayFabClientAPI.UpdateUserData(request,
                        result =>
                        {
                            Debug.Log("UserData updated successfully");
                            // Aquí puedes manejar cualquier acción adicional después de la actualización
                        },
                        error =>
                        {
                            Debug.LogError("UpdateUserData error: " + error.GenerateErrorReport());
                        });
                }
                else
                {
                    Debug.LogError("UserData not found for key: " + sceneName);
                }
            },
            error =>
            {
                Debug.LogError("GetUserData error: " + error.GenerateErrorReport());
            });
    }
}
