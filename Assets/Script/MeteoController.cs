using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoController : MonoBehaviour
{
    private int score;
    private int totalScore;
    private int wrong;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        totalScore = 0;
        wrong = 0;
    }
    public void SetScore(int score)
    {
        if (score == -5)
        {
            wrong++;
        }
        this.score += score;
        totalScore++;
    }

    public void GetCanvasQuestion(GameObject canvasClouds)
    {
        StartCoroutine(_CanvasQuestion(canvasClouds));
    }

    IEnumerator _CanvasQuestion(GameObject canvasClouds)
    {
        yield return new WaitForSeconds(1.5f);

        canvasClouds.SetActive(true);
        SetCursor(true);
    }
    public void SetCursor(bool visible)
    {
        Cursor.visible = visible;
    }
}
