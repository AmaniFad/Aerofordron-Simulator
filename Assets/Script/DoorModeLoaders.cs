using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorModeLoaders : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;



    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.SceneLoad(sceneToLoad);
    }
}
