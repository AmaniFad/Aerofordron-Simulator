using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerExamen : Timer
{
    public static TimerExamen Instance;
    [SerializeField] private bool isTime;

    [SerializeField] private UnityEvent _TimeFinish;

    private void Start()
    {
        Instance = this;
    }
    public void SetTime(bool time)
    {
        isTime = time;
    }
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
