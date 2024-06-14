using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineWhenSelected : MonoBehaviour
{
    private Outline currentOutline;
    [SerializeField] private GameObject outsideObject;
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

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            if (TryGetComponent<Outline>(out Outline outline))
            {
                

                    currentOutline = outline;
                    outline.OutlineWidth = 10;
                    if (TryGetComponent<Image>(out Image image))
                {
                    Color previousColor = image.color;
                    previousColor.a = 255;
                    image.color = previousColor;
                }
                
            }
            else
            {
                currentOutline = gameObject.AddComponent<Outline>();
                currentOutline.OutlineWidth = 10;
                if (TryGetComponent<Image>(out Image image))
                {
                    Color previousColor = image.color;
                    previousColor.a = 255;
                    image.color = previousColor;
                }
            }
        }
        else
        {
            if (currentOutline)
            currentOutline.OutlineWidth = 0;
            if (TryGetComponent<Image>(out Image image))
            {
                Color previousColor = image.color;
                previousColor.a = 0;
                image.color = previousColor;
            }
        }

    }

}
