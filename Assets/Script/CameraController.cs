using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera droneCamera;

    //Para un projecto mas grande con mas camaras esto no se haria asi, esto es bastante cutre pero nos sirve
    void Update()
    {
        if (DronInputController.Instance.CanChangeCamera())
        {
            if (playerCamera.Priority == 11)
            {
                playerCamera.Priority = 10;
                droneCamera.Priority = 11;
            }
            else
            {
                playerCamera.Priority = 11;
                droneCamera.Priority = 10;
            }
                DronInputController.Instance.HasChangedCamera();
        }
    }
}
