using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCity : Timer
{
    private bool startGame;

    public void SetStartGame(bool startGame)
    {
        this.startGame = startGame;
    }
    
    void Update()
    {
        if(startGame)
        {
            base.cuentaAdelante();
        }
    }
}
