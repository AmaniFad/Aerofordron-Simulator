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
        tabletPlaceholder.SetActive(false);
        hudTablet.SetActive(true);
        PlayerStateController.instance.StopCameraMovement();
        PlayerStateController.instance.StopMoving();
        Cursor.visible = true;
    }

    public void RespawnTablet()
    {
        tabletPlaceholder.SetActive(true);
    }

}
