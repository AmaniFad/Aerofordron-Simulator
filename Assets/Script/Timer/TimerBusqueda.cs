using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBusqueda : Timer
{
    [Header("Canva")]
    [SerializeField] private GameObject canvaLose;
    [SerializeField] private TMP_Text timerTextCanvaNext;
    [SerializeField] private GameObject canvasSeBusca;

    [Header("CountDownTimer")]
    [SerializeField] private GameObject canvasCountDown;
    [SerializeField] private int startCountdownFrom = 3;  
    [SerializeField] private TMP_Text countdownText;
    private float countdownTime;

    public static TimerBusqueda instance;
    private bool startGame;
    public void SetStartGame(bool startGame)
    {
        this.startGame = startGame;
    }
    public void SetRemainingTime(int remainingTime)
    {
        this.remainingTime = remainingTime;
    }
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        StartCountDown();
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
            if (remainingTime <= 0)
            {
                Lose();
            }
        }
    }
    public void StartCountDown()
    {
        countdownTime = startCountdownFrom;
        canvasCountDown.SetActive(true);
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString("0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownText.text = "GO!";
        canvasCountDown.SetActive(false);
        timerText.gameObject.SetActive(true);
        canvasSeBusca.SetActive(true);
        elapsedTime = 0; 
        SetStartGame(true);
        BusquedaObjetosController.instance.SetObjectInSpawn();
    }
    private void Lose()
    {
        SetStartGame(false);
        canvaLose.SetActive(true);
        StopTime();
    }
    public void setTextTimeNext()
    {
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        timerTextCanvaNext.text = string.Format("{0:00}:{1:00}", min, sec);
        BusquedaObjetosController.instance.SetFinalTime(string.Format("{0:00}:{1:00}", min, sec));
    }
}
