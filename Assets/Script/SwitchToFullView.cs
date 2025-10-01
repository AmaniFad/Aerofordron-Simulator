using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject[] fullViewCameras;
    private bool currentState = false;
    private float changeCameraCooldown = 0.2f;
    private bool canChangeCamera;
    private void Start()
    {
        instance = this;
        canChangeCamera = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (DisplayInputData.isChangeCameraPressed && !PlayerStateController.instance.CanMove())
        {

                StartCoroutine(DoChangeCameraCooldown());
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
        fullViewCameras[1].GetComponent<Camera>().enabled = true;
        fullViewCameras[0].GetComponent<Camera>().enabled = false;
    }

    public void ExitFullView()
    {
        fullViewCameras[1].GetComponent<Camera>().enabled = false ;
        fullViewCameras[0].GetComponent<Camera>().enabled = true ;
    }

    private IEnumerator DoChangeCameraCooldown()
    {
        canChangeCamera = false;
        yield return new WaitForSeconds(changeCameraCooldown);
        canChangeCamera = true;
    }
}
