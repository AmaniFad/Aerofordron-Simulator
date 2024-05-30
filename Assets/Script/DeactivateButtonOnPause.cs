 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateButtonOnPause : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0.1f)
        {
            GetComponent<Button>().interactable = (false);
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
