using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Poster : MonoBehaviour
{
    [SerializeField] private UnityEvent onPasPosterMC;
    [SerializeField] private UnityEvent onPasPosterP;

    [SerializeField] private GameObject nextPoster;
    [SerializeField] private Image image;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (TimerModoCarrera.instance.GetModoCarrera())
            {
                onPasPosterMC.Invoke();
                changeColors();
            }
            else
            {
                onPasPosterP.Invoke();
                changeColors();
            }
        }
    }

    private void changeColors()
    {
        if (nextPoster != null) 
        {
            nextPoster.GetComponent<BoxCollider>().enabled = true;
            nextPoster.GetComponentInChildren<Image>().color = Color.blue;
        }
        image.color = Color.yellow;
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
