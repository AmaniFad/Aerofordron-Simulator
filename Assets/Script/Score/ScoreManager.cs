using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] private int maximScore;
    [SerializeField] private UnityEvent<int> OnUpdateCanvasScore;
    [SerializeField] private UnityEvent OnWin;

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
        maximScore = SpawnCPController.Instance.GetCpTotal();
    }

    public void UpdateScore(int scoreM)
    {
        score += scoreM;
        OnUpdateCanvasScore.Invoke(score);
        if(score == maximScore)
        {
            OnWin.Invoke();
        }
    }
}
