using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class ActivateSpotlightOnSelected : MonoBehaviour
{
    [SerializeField] GameObject spotLight;
    void Start()
    {
        
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            spotLight.SetActive(true);
        }
        else
        {
            spotLight.SetActive(false);
        }

    }

}
