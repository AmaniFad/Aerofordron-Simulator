using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public int maximScore;
    public UnityEvent<int> OnUpdateCanvasScore;

    private void OnEnable()
    {
        ScoreController.OnUpdateScore += UpdateScore;
    }
    private void OnDisable()
    {
        ScoreController.OnUpdateScore -= UpdateScore;
    }

    public void Start()
    {
        score = 0;
    }

    public void UpdateScore(int scoreM)
    {
        score += scoreM;
        OnUpdateCanvasScore.Invoke(score);
    }
}
