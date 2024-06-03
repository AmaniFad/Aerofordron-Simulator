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
                
            }
            else
            {
                currentOutline = gameObject.AddComponent<Outline>();
                currentOutline.OutlineWidth = 10;
            }
        }
        else
        {
            if (currentOutline)
            currentOutline.OutlineWidth = 0;
        }

    }

}
