using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTooltip : MonoBehaviour
{
    [SerializeField] private GameObject interactable;
    [SerializeField] private GameObject toolTip;
    private GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        { 
            if (interactable.activeInHierarchy)
            {
                interactable.transform.LookAt(player.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        toolTip.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        toolTip.SetActive(false);
    }

}
