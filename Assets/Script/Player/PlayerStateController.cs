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
        //staticCamera.transform.position = Camera.main.transform.position;
        //staticCamera.transform.rotation = Camera.main.transform.rotation;
        playerCamera.gameObject.SetActive(false);
        //staticCamera.gameObject.SetActive(true);
    }

    public void ResumeCameraMovement()
    {
        playerCamera.gameObject.SetActive(true);
        //staticCamera.gameObject.SetActive(false);
    }

    public void CameraToDron(GameObject dron)
    {
        if (dron != null)
        {
            dronCamera.Priority = 11;
            dronCamera.LookAt = dron.transform;
            playerCamera.Priority = 10;
        }
    }

    public void CameraToPlayer()
    {


        playerCamera.Priority = 11;
        dronCamera.Priority = 10;


    }
}
