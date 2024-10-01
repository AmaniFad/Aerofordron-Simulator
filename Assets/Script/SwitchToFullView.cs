using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;
    private bool currentState = false;

    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (DisplayInputData.isChangeCameraPressed)
        {
            currentState = !currentState;
            if (currentState)
            {

                EnterFullView();
            }
            else
            {
                ExitFullView();
            }
        }
    }

    public void EnterFullView()
    {
        fullViewCamera.SetActive(true);
    }

    public void ExitFullView()
    {
        fullViewCamera.SetActive(false);
    }
}
