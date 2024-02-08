using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchPoint : MonoBehaviour
{
   [SerializeField] private UnityEvent OnTouch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            OnTouch.Invoke();
        }
    }
}
