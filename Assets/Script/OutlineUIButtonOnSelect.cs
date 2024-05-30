using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineUIButtonOnSelect : MonoBehaviour
{
    private UnityEngine.UI.Outline currentOutline;
    [SerializeField] private GameObject outsideObject;
    [SerializeField] private bool isOutlineObjectNotThis;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            Debug.Log("Enters");
            if (TryGetComponent<UnityEngine.UI.Outline>(out UnityEngine.UI.Outline outline))
            {


                currentOutline = outline;


            }
            else
            {
                currentOutline = gameObject.AddComponent<UnityEngine.UI.Outline>();
            }
        }
        else
        {
            if (currentOutline)
            {

            }
        }

    }
}
