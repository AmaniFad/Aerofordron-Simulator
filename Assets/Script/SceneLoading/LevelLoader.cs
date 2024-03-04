using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance;
    private string currentLevel;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Este singleton ya exisate borrando objeto:" + gameObject.name);
            Destroy(gameObject);
        }
    }
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        currentLevel = sceneName;
    }

    public void UnloadLevel(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadCurrentLevel()
    {
        SceneManager.UnloadSceneAsync(currentLevel);
    }

    public string GetCurrentLevel()
    {
        return currentLevel;
    }
}
