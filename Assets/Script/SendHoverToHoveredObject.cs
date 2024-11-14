using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class SendHoverToHoveredObject : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {


    }
    public void OnHoverEntered(UIHoverEventArgs args)
    {
        print("OnHover");
        args.uiObject.GetComponent<HighlightSignOsHover>().Hovered();
    }

    public void OnHoverExited(UIHoverEventArgs args)
    {
        print("OnExitHover");
        args.uiObject.GetComponent<HighlightSignOsHover>().Unhovered();
    }

}
