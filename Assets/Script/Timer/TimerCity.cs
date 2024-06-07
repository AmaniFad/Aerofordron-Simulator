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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame)
        {
            base.cuentaAdelante();
        }
    }
}
