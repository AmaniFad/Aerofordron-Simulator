using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBusqueda : Timer
{
    [Header("Canva")]
    [SerializeField] private GameObject canvaLose;

    private bool startGame;

    public void SetStartGame(bool startGame)
    {
        this.startGame = startGame;
    }
    public void SetRemainingTime(int remainingTime)
    {
        this.remainingTime = remainingTime;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (startGame)
        {
            base.cuentaAtras(); 

            if (remainingTime < 14)
            {
                timerText.color = Color.red;
            }
            if (elapsedTime <= 0)
            {
                Lose();
            }
        }
    }

    private void Lose()
    {
        canvaLose.SetActive(true);
        StopTime();
    }
}
