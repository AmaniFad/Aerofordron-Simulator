using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VInspector.Libs;

public class Poster : MonoBehaviour
{
    [SerializeField] private UnityEvent onPasPosterMC;
    [SerializeField] private UnityEvent onPasPosterP;

    [SerializeField] private GameObject nextPoster;
    [SerializeField] private SpriteRenderer image;
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
            var sr = nextPoster.GetComponentInChildren<SpriteRenderer>();
            Color c2 = Color.blue;
            c2.a = 0.4f;
            sr.color = c2;
        }
        image.color = Color.yellow;
        image.color = image.color.SetAlpha(.4f);
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
