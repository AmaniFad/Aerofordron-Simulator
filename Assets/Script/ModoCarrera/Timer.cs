using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float remainingTime;

    private float elapsedTime;
    private bool modoCarrera;

    public void SetModoCarrera(bool modoCarrera)
    {
        this.modoCarrera = modoCarrera;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (modoCarrera)
        {
            remainingTime -= Time.deltaTime;
            int min = Mathf.FloorToInt(remainingTime / 60);
            int sec = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
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
