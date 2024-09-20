using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDeBusqueda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BusquedaObjetosController.instance.ObjectFounded();
        }
    }
}
