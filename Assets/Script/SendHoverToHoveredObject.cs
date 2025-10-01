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
        args.uiObject.GetComponent<HighlightSignOnHover>().Hovered();
    }

    public void OnHoverExited(UIHoverEventArgs args)
    {
        print("OnExitHover");
        args.uiObject.GetComponent<HighlightSignOnHover>().Unhovered();
    }

    public void OnPhyisicalHoverEntered(HoverEnterEventArgs args)
    {
        print("OnHover");
        args.interactableObject.transform.gameObject.GetComponent<HighlightSignOnHover>().Hovered();
    }

    public void OnPhysicalHoverExited(HoverExitEventArgs args)
    {
        print("OnExitHover");
        args.interactableObject.transform.gameObject.GetComponent<HighlightSignOnHover>().Unhovered();
    }

}
