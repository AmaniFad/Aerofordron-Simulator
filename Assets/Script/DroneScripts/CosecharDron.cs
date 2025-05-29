using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosecharDron : MonoBehaviour
{
    [SerializeField] private GameObject[] agua;
    private void Start()
    {
        foreach (GameObject go in agua)
        {
            go.SetActive(false);
        }
    }
    void Update()
    {
        SoltarAgua();
    }
    public void SoltarAgua()
    {
        bool chorro = DisplayInputData.isPrimaryPressed;
        if (chorro)
        {
            foreach (GameObject go in agua)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in agua)
            {
                go.SetActive(false);
            }
        }
    }

}
