using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader Instance { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Ya existe otra instancia de SceneLoader");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); 

    }
    public void SceneLoad(string scene)
    {
        SceneManager.LoadSceneAsync(scene,LoadSceneMode.Single);
    }

    public void AddScene(string scene)
    {
        SceneManager.LoadScene(scene,LoadSceneMode.Additive);
    }

    public void RemoveScene(string scene) 
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
