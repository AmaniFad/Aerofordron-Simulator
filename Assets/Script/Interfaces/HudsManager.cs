using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HudsManager : MonoBehaviour
{
    [SerializeField] private GameObject todosLosCanvas;
    private void Update()
    {
        if (this.gameObject.GetComponent<PauseController>().GetISPause())
        {
            todosLosCanvas.SetActive(false);
        }
        else
        {
            todosLosCanvas.SetActive(true);
        }
    }
}
