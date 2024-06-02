using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float remainingTime;

    [Header("Canva")]
    [SerializeField] private GameObject canvaLose;
    [SerializeField] private TMP_Text timerTextCanvaWin;
    private float elapsedTime;
    public bool modoCarrera;
    public static Timer instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            if (modoCarrera)
            {
                remainingTime -= Time.deltaTime;
                int min = Mathf.FloorToInt(remainingTime / 60);
                int sec = Mathf.FloorToInt(remainingTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", min, sec);
                elapsedTime += Time.deltaTime;

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
                elapsedTime += Time.deltaTime;
                int min = Mathf.FloorToInt(elapsedTime / 60);
                int sec = Mathf.FloorToInt(elapsedTime % 60);
                timerText.text = string.Format("{0:00}:{1:00}", min, sec);
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
    public void RestartTime()
    {
        Time.timeScale = 1.0f; 
        Cursor.visible = false;
    }
    public void StopTime()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }
    public void setTextTime()
    {
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        timerTextCanvaWin.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
