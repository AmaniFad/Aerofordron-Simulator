using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModeLoaders : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private bool canTrigger;


    private void Start()
    {
        canTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger)
        {
            SceneLoader.Instance.SceneLoad(sceneToLoad);
            canTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canTrigger = true;
    }
}
