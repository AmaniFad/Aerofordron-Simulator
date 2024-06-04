using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Poster : MonoBehaviour
{
    [SerializeField] private UnityEvent onPasPosterMC;
    [SerializeField] private UnityEvent onPasPosterP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Timer.instance.GetModoCarrera())
            {
                onPasPosterMC.Invoke();
            }
            else
            {
                onPasPosterP.Invoke();
            }
        }
    }
}
