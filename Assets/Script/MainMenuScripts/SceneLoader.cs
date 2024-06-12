using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransitions;
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

    }
    public void SceneLoad(string scene)
    {
        if (sceneTransitions != null)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }
        else
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        GameObject instanceForControl = Instantiate(new GameObject());
        instanceForControl.AddComponent<AlwaysLookAtGameobject>().StartCoroutine(_TransitionControl(scene,instanceForControl));  
        DontDestroyOnLoad(instanceForControl);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator _TransitionControl(string scene, GameObject transitionController)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        GameObject b = Instantiate(sceneTransitions);
        DontDestroyOnLoad(b);
        b.GetComponent<Animator>().SetTrigger("leaveTransition");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Works");
        b.GetComponent<Animator>().SetTrigger("enterTransition");
        Destroy(transitionController);
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
