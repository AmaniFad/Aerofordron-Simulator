using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTablet : MonoBehaviour, IInteractable
{
    //Aqui se pone la tablet que esta dentro de la camera para que aparezca
    [SerializeField] private GameObject hudTablet;
    public void Interact()
    {
        Debug.Log("Interacted");
        this.gameObject.SetActive(false);
        hudTablet.SetActive(true);
    }

}
