using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightOnHover : MonoBehaviour
{
    private Outline currentOutline;
    [SerializeField] private GameObject outsideObject;
    [SerializeField] private TextMeshProUGUI signText;
    [SerializeField] private bool isOutlineObjectNotThis;

    public void Hovered()
    {
        print("Hovered");

        if (!TryGetComponent<Outline>(out Outline outline))
        {


            currentOutline = gameObject.AddComponent<Outline>();
            currentOutline.OutlineWidth = 20;

        }
        else
        {

            currentOutline = outline;
            outline.OutlineWidth = 10;
        }
        EventSystem.current.SetSelectedGameObject(this.gameObject);


    }

    public void Unhovered()
    {
        if (currentOutline)
        {

            currentOutline.OutlineWidth = 0;
        }
    }
}
