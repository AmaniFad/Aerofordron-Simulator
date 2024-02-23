using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController instance { get; private set; }
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera staticCamera;
    [SerializeField] CinemachineVirtualCamera dronCamera;
    [SerializeField] private GameObject player;
    private bool canMove;
    // Start is called before the first frame update

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        ResumeMoving();
    }

    public void StopMoving()
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        canMove = false;
    }

    public void ResumeMoving()
    {
        canMove = true;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void StopCameraMovement()
    {
        playerCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        playerCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;


    }

    public void ResumeCameraMovement()
    {
        playerCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 150;
        playerCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 300;


    }

    public void CameraToDron(GameObject dron)
    {
        if (dron != null)
        {
            dronCamera.Priority = 11;
            dronCamera.LookAt = dron.transform;
            playerCamera.Priority = 10;
        }
        else
        {
            dronCamera.Priority = 10;
            playerCamera.Priority = 11;
        }
    }

    public void CameraToPlayer()
    {
        playerCamera.Priority = 11;
        dronCamera.Priority = 10;
    }
    public CinemachineVirtualCamera GetDronCamera()
    {
        return dronCamera;
    }
}
