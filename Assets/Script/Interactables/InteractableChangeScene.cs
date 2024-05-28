using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableChangeScene :MonoBehaviour, IInteractable
{
    [SerializeField] private string newSceneName;
    [SerializeField] private GameObject tooltip;
    public void DropInteractable()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        SceneManager.LoadScene(newSceneName);
    }

}
