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
        if (other.CompareTag("Player"))
        { 
            onCheckPoint.Invoke();
            StartCoroutine(_isCP());
        }
        else if (other.CompareTag("Dron"))
        {
            onCheckPoint.Invoke();
        }
    }

    IEnumerator _isCP()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerStateController.instance.StopMoving();
        PlayerStateController.instance.StopCameraMovement();
        Cursor.visible = true;
    }

    public void CheckPointPassed()
    {
        PlayerStateController.instance.ResumeMoving();
        PlayerStateController.instance.ResumeCameraMovement();
        Cursor.visible = false;
        Destroy(gameObject);
    }
}
