using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDeBusqueda : MonoBehaviour
{
    [SerializeField] private GameObject canvasNext;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
