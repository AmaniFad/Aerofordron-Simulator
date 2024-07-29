using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    public static SwitchToFullView instance;
    [SerializeField] private GameObject fullViewCamera;

    private void Start()
    {
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
        fullViewCamera.SetActive(true);
    }

    public void ExitFullView()
    {
        fullViewCamera.SetActive(false);

    }
}
