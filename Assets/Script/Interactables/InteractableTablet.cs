using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTablet : MonoBehaviour, IInteractable
{
    //Aqui se pone la tablet que esta dentro de la camera para que aparezca
    [SerializeField] private GameObject hudTablet;

    [SerializeField] private GameObject tabletPlaceholder;
    public void Interact()
    {
        Debug.Log("Interacted");
        tabletPlaceholder.SetActive(false);
        hudTablet.SetActive(true);
    }

}
