using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private TMP_Text rongAnswers_Text;
    [SerializeField] private TMP_Text aproveTheoric_Text;

    private int scoreQuiz;
    private int scoreInteractive;

    private int totalScore;
    private int totalScoreQuiz;

    private bool aproveTheoric;

    private int rongAnswers;
    public void SetScoreQuiz(int scoreQuiz)
    {
        this.scoreQuiz = scoreQuiz;
    }
    public void SetScoreInteractive(int scoreInteractive)
    {
        this.scoreInteractive = scoreInteractive;
    }

    public void SetTotalScoreQuiz(int totalScoreQuiz)
    {
        this.totalScoreQuiz = totalScoreQuiz;
    }

    void Start()
    {
        scoreInteractive = 0;
        scoreQuiz = 0;
    }

    public void PlusScoreInteractive(int scoreInteractive)
    {
        this.scoreInteractive += scoreInteractive;
    }

    public void CheckTheoricRequisites()
    {
        int percentageOfTotalScoreQuiz = (scoreQuiz * 100) / totalScoreQuiz;

        if(percentageOfTotalScoreQuiz >= 80)
        {
            aproveTheoric = true;
        }

        rongAnswers = (totalScoreQuiz / 10) - (scoreQuiz / 10);
    }

    public void SetInfoText()
    {
        CheckTheoricRequisites();

        score_Text.text = scoreQuiz.ToString();
        rongAnswers_Text.text = rongAnswers.ToString();
        if(aproveTheoric)
        {
            aproveTheoric_Text.text = "COMPLETADO";
        }
        else
        {
            aproveTheoric_Text.text = "NO COMPLETADO";
        }
    }
}
