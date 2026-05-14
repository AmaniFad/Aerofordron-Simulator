using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;
    [SerializeField] private GameObject hudPlayer;

    private void Start()
    {
        hudPlayer = PlayerReferences.instance.GetHUD();
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerInputController.Instance.IsFullView() && !PlayerStateController.instance.CanMove())
        {
            EnterFullView();
        }
        else
        {
            ExitFullView();
        }
    }

    public void EnterFullView()
    {
        Camera.main.gameObject.SetActive(false);
        fullViewCamera.SetActive(true);
        hudPlayer.SetActive(false);
    }

    public void ExitFullView()
    {
        Camera.main.gameObject.SetActive(true);
        fullViewCamera.SetActive(false);
        hudPlayer.SetActive(true);
    }
}
