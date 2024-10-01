using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float remainingTime;
    public TMP_Text timerTextCanvaWin;

    public float elapsedTime;
    public void cuentaAtras()
    {
        remainingTime -= Time.deltaTime;
        int min = Mathf.FloorToInt(remainingTime / 60);
        int sec = Mathf.FloorToInt(remainingTime % 60);
        if(timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        elapsedTime += Time.deltaTime;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void cuentaAdelante()
    {
        elapsedTime += Time.deltaTime;
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
    }

    public void setTextTime()
    {
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        timerTextCanvaWin.text = string.Format("{0:00}:{1:00}", min, sec);
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.SetFinalTime(string.Format("{0:00}:{1:00}", min, sec));
        }
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
}
