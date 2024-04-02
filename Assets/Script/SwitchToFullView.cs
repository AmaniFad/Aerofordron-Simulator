using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToFullView : MonoBehaviour
{
    [SerializeField] private GameObject fullViewCamera;

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputController.Instance.IsFullView())
        {
            fullViewCamera.SetActive(true);
        }
        else
        {
            fullViewCamera.SetActive(false);
        }
    }
}
