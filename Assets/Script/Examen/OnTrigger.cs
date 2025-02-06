using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _Event;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PLayer"))
        {
            _Event.Invoke();
        }
    }
}
