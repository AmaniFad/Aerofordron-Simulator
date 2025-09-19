using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HudsManager : MonoBehaviour
{
    [SerializeField] private PauseController pauseController;
    private void Update()
    {
        if (pauseController.GetISPause())
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
