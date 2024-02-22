using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronZoom : MonoBehaviour
{
    [SerializeField] private float minDistanceForZoom;
    private GameObject dron;
    private GameObject player;
    private CinemachineVirtualCamera dronCamera;
    private int frameCounter;
    // Start is called before the first frame update
    void Start()
    {
        dronCamera = PlayerStateController.instance.GetDronCamera();
        player = PlayerReferences.instance.GetPlayer();
    }

    private void Update()
    {
        frameCounter++;

        if (frameCounter % 5 == 0)
        {
            CheckIfZoom();
            frameCounter = 0;
        }
    }

    private void CheckIfZoom()
    {
        dron = PlayerReferences.instance.GetDron();
        if (dron != null)
        {
            float distance = Vector3.Distance(player.transform.position, dron.transform.position);
            float currentMinFOV = dronCamera.GetComponent<CinemachineFollowZoom>().m_MinFOV;

            // Calculate the percentage decrease based on distance
            float decreasePercentage = (distance - minDistanceForZoom) / currentMinFOV;
            Debug.Log(decreasePercentage);
            // Calculate the new minimum FOV with percentage decrease
            float newMinFOV = currentMinFOV * (1 - decreasePercentage); // Decrease relative to current FOV

            // Ensure the new minimum FOV stays within a specified range
            float minAllowedFOV = 30f; // Adjust as needed
            float maxAllowedFOV = 60f; // Adjust as needed
            newMinFOV = Mathf.Clamp(newMinFOV, minAllowedFOV, maxAllowedFOV);

            // Update the minimum FOV
            dronCamera.GetComponent<CinemachineFollowZoom>().m_MinFOV = newMinFOV;

        }
    }
}
