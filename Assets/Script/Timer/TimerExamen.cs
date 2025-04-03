using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerExamen : Timer
{
    public static TimerExamen Instance;
    [SerializeField] private bool isTime;
    [SerializeField] private bool isLevel8;

    [SerializeField] private UnityEvent _TimeFinish8;
    [SerializeField] private UnityEvent _TimeFinish9;

    private void Start()
    {
        Instance = this;
    }
    public void SetTime(bool time)
    {
        isTime = time;
    }
    public void SetLevel(bool level)
    {
        this.isLevel8 = level;
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
                if (isLevel8)
                {
                    _TimeFinish8.Invoke();
                }
                else
                {
                    _TimeFinish9.Invoke();
                }
            }
        }
    }
}
