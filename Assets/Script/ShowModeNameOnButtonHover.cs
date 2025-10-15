using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowModeNameOnButtonHover : MonoBehaviour
{
    [SerializeField] GameObject modeName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        //{
        //    modeName.SetActive(true);
        //}
        //else
        //{
        //    modeName.SetActive(false);
        //}

    }

    public void ShowName()
    {
        modeName.SetActive(true);
    }

    public void HideName()
    {
        modeName.SetActive(false);
    }
}
