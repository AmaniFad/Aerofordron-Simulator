using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;
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
        if (DisplayInputData.isChangeCameraPressed)
        {
            if (canChangeCamera)
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
    }

    public void EnterFullView()
    {
        fullViewCamera.SetActive(true);
    }

    public void ExitFullView()
    {
        fullViewCamera.SetActive(false);
    }

    private IEnumerator DoChangeCameraCooldown()
    {
        canChangeCamera = false;
        yield return new WaitForSeconds(changeCameraCooldown);
        canChangeCamera = true;
    }
}
