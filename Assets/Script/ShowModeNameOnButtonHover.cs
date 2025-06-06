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


    }

    public void ShowModeName()
    {
        modeName.SetActive(true);
    }

    public void HideModeName()
    {
        modeName.SetActive(false);
    }
}
