using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera mainCamera;
    [SerializeField]
    private CinemachineVirtualCamera flySelectionCamera;

    [SerializeField]
    private GameObject mainButtons;
    [SerializeField]
    private GameObject flyButtons;

    private CinemachineVirtualCamera currentSelectedCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToFlySelection()
    {
        mainButtons.SetActive(false);
        flyButtons.SetActive(true);
    }

    public void ChangeToMainSelection()
    {
        mainButtons.SetActive(true);
        flyButtons.SetActive(false);
    }
    public void ChangeToFlySelectionCamera()
    {
        mainCamera.Priority = 9;
        flySelectionCamera.Priority = 10;
        ChangeToFlySelection();
        currentSelectedCamera = flySelectionCamera;
    }

    public void ChangeToMainCamera()
    {
        currentSelectedCamera.Priority = 9;
        mainCamera.Priority = 10;
        currentSelectedCamera = mainCamera;
    }
}
