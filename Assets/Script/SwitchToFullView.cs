using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;
    private bool isActive;
    private bool canSwitch;

    private void Start()
    {
        isActive = false;
        instance = this;
        canSwitch = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (DisplayInputData.isChangeCameraPressed && !PlayerStateController.instance.CanMove() && canSwitch)
        {
            ToggleFullView();
            StartCoroutine(CooldownToSwitch());
        }

    }

    public void ToggleFullView()
    {
        isActive = !isActive;
        fullViewCamera.SetActive(isActive);
    }
    public void EnterFullView()
    {
        fullViewCamera.SetActive(true);
    }

    public void ExitFullView()
    {
        fullViewCamera.SetActive(false);
    }

    private IEnumerator CooldownToSwitch()
    {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canSwitch = true;
    }
}
