using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public static LevelLoader Instance;
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
    }

    public void UnloadLevel(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
