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
    [SerializeField] private string nameLevel;
    private float finalTime;

    public static ScoreManager instance;

    private void OnEnable()
    {
        ScoreController.OnUpdateScore += UpdateScore;
    }
    private void OnDisable()
    {
        ScoreController.OnUpdateScore -= UpdateScore;
    }
    
    public void SetFinalTime(float finalTime)
    {
        this.finalTime = finalTime;
    }

    public void Start()
    {
        score = 0;
        maximScore = SpawnCPController.Instance.GetCpTotal();

        if(instance != null)
        {
            instance = this;
        }
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
    
    public void SaveScore()
    {
        PlayFabManager.Instance.ComprobeTitleData(nameLevel, score, finalTime);
    }
}
