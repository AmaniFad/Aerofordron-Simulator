using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera droneCamera;
    void Update()
    {
        if (InputController.Instance.CanChangeCamera())
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
                InputController.Instance.HasChangedCamera();
        }
    }
}
