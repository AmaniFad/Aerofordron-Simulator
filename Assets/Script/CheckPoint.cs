using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent onCheckPoint;
    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
        onCheckPoint.Invoke();
        StartCoroutine(_isCP());
    }

    IEnumerator _isCP()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0f;
    }

    public void CheckPointPassed()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
