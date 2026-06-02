using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;
    [SerializeField] private GameObject hudPlayer;
    private GameObject oldCamera;
    private bool fullview;
    private void Start()
    {
        hudPlayer = PlayerReferences.instance.GetHUD();
        instance = this;
        oldCamera = Camera.main.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerInputController.Instance.IsFullView() && !PlayerStateController.instance.CanMove() && !fullview)
        {
            EnterFullView();
            fullview = true;
        }
        else if (fullview && !PlayerInputController.Instance.IsFullView())
        {
            fullview = false;
            ExitFullView();
        }
    }

    public void EnterFullView()
    {
        oldCamera.SetActive(false);
        fullViewCamera.SetActive(true);
        hudPlayer.SetActive(false);
    }

    public void ExitFullView()
    {
        oldCamera.SetActive(true);
        fullViewCamera.SetActive(false);
        hudPlayer.SetActive(true);
    }
}
