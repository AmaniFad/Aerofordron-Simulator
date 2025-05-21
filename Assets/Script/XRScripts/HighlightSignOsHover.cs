using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightSignOsHover : MonoBehaviour
{
    private Outline currentOutline;
    [SerializeField] private GameObject outsideObject;
    [SerializeField] private TextMeshProUGUI signText;
    [SerializeField] private bool isOutlineObjectNotThis;
    void Start()
    {
        if (TryGetComponent<Image>(out Image image))
        {
            Color previousColor = image.color;
            previousColor.a = 0;
            image.color = previousColor;
        }
    }
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
                signText.color = Color.white;
                outline.OutlineWidth = 10;
            }
            if (TryGetComponent<Image>(out Image image))
            {
                Color previousColor = image.color;
                //previousColor.a = 255;
                image.color = previousColor;
            }

        
    }

    public void Unhovered()
    {
        if (currentOutline)
        {

            currentOutline.OutlineWidth = 0;
            signText.color = Color.black;
        }
        if (TryGetComponent<Image>(out Image image))
        {
            Color previousColor = image.color;
            previousColor.a = 0;
            image.color = previousColor;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
