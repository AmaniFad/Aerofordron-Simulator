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
    public float ScoreTime;
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

    public void ComprobeTitleData(string sceneName, int score, float scoreTime)
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
    public void SaveInfoPlayer(string sceneName, int score, float scoreTime)
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

    public void UpdateInfoPlayer(string sceneName, int score, float scoreTime)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest { Keys = new List<string> { sceneName } },
            dataResult =>
            {
                var playerValuesJson = dataResult.Data[sceneName].Value;
                var playerValuesList = JsonUtility.FromJson<ScoreValues>(playerValuesJson);

                playerValuesList.Score = score;
                playerValuesList.ScoreTime = scoreTime;

                var request = new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string>
                    {
                        { 
                            sceneName, 
                            JsonUtility.ToJson(playerValuesList)
                        }
                    }
                };
            }, error => { });
    }
}
