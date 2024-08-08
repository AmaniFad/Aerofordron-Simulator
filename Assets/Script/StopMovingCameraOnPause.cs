using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingCameraOnPause : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float Xspeed;
    private float Yspeed;
    private bool hasGotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        hasGotSpeed = false;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        Xspeed = virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
        Yspeed = virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (!hasGotSpeed)
            {
                Xspeed = virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed;
                Yspeed = virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed;

                hasGotSpeed = true;
            }
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
        }
        else
        {
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = Yspeed;
            virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = Xspeed;
        }
    }
}
