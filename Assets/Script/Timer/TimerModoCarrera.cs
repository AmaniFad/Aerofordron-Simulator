using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerModoCarrera : Timer
{
    [Header("Canva")]
    [SerializeField] private GameObject canvaLose;
    [SerializeField] private GameObject canvasWin;
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private PauseController pauseMenu;


    public bool modoCarrera;
    public static TimerModoCarrera instance;

    private float saveRemainingTime;
    private bool startGame;
    public void SetModoCarrera(bool modoCarrera)
    {
        this.modoCarrera = modoCarrera;
    }
    public bool GetModoCarrera()
    {
        return modoCarrera;
    }
    public void SetStartGame(bool startGame)
    {
        this.startGame = startGame;
    }
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        saveRemainingTime = remainingTime;
        elapsedTime = 0;
        StopTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            if (modoCarrera)
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
            else
            {
                base.cuentaAdelante();
            }
        }
    }
    private void Lose()
    {
        canvaLose.SetActive(true);
        StopTime();
    }
    public void RestartMC()
    {
        remainingTime = saveRemainingTime;
    }
    public void RestarP()
    {
        elapsedTime = 0;
    }

    public void EndLap()
    {
        canvasWin.SetActive(true);
        StopTime();
        setTextTime();
        timerCanvas.SetActive(false);
        pauseMenu.PauseWihoutPauseMenu();
    }
}
