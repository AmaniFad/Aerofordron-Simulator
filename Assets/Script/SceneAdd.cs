using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdd : MonoBehaviour
{

    string previousScene;
    public void AddScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += RemoveScene;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void RemoveScene(Scene scene, LoadSceneMode mode) 
    {
        SceneManager.sceneLoaded -= RemoveScene;
        SceneManager.SetActiveScene(scene);
        SceneManager.UnloadSceneAsync(previousScene);
    }

    public void LoadSceneFromLoader(string sceneName)
    {
        SceneLoader.Instance.SceneLoad(sceneName);
    }
}
