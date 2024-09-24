using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosecharDron : MonoBehaviour
{
    [SerializeField] private GameObject agua;
    private void Start()
    {
        agua.SetActive(false);
    }
    void Update()
    {
        SoltarAgua();
    }
    public void SoltarAgua()
    {
        float chorro = DronInputController.Instance.GetAguaInput();
        if (chorro == 1)
        {
            agua.SetActive(true);
        }
        else
        {
            agua.SetActive(false);
        }
    }

}
