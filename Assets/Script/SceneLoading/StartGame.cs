using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static StartGame Instance;



    public void InstantiatePlayer()
    {
        SceneLoader.Instance.AddScene("PlayerScene");
        StartCoroutine(_WaitForRemoval());
    }

    private IEnumerator _WaitForRemoval()
    {
        yield return new WaitForEndOfFrame();
        SceneLoader.Instance.RemoveScene("PlayerScene");
    }
}
