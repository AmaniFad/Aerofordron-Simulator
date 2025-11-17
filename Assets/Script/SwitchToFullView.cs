using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using VInspector;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private LayerMask fullviewCameraCulling;
    [SerializeField] private LayerMask normalViewCameraCulling;
    [SerializeField] private LayerMask fullViewVolumeCulling;
    [SerializeField] private LayerMask normalViewVolumeCulling;
    
    private bool currentState = false;
    private float changeCameraCooldown = 0.2f;
    private bool canChangeCamera;
    [SerializeField]UnityEngine.SpatialTracking.TrackedPoseDriver trackedPoseDriver;
    
    private void Start()
    {
        instance = this;
        canChangeCamera = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (DisplayInputData.isChangeCameraPressed && !PlayerStateController.instance.CanMove() && canChangeCamera)
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

        Camera.main.cullingMask = fullviewCameraCulling;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        currentState = true;
    }

    public void ExitFullView()
    {
        Camera.main.cullingMask = normalViewCameraCulling;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        currentState = false;
    }

    [Button]
    public void ToggleFullView()
    {
        if (!currentState)
        {
            Camera.main.cullingMask = fullviewCameraCulling;
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            currentState = true;

        }
        else
        {
            Camera.main.cullingMask = normalViewCameraCulling;
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            currentState = false;
        }
    }
    private IEnumerator DoChangeCameraCooldown()
    {
        canChangeCamera = false;
        yield return new WaitForSeconds(changeCameraCooldown);
        canChangeCamera = true;
    }


}
