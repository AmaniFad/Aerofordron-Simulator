using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerExamen : Timer
{
    [SerializeField] private bool isTime;

    [SerializeField] private UnityEvent _TimeFinish;
    void Update()
    {
        if (isTime)
        {
            base.cuentaAtras();
            if(remainingTime <= 0)
            {
                isTime = false;
                timerText.gameObject.SetActive(false);
                _TimeFinish.Invoke();
            }
        }
    }
}
