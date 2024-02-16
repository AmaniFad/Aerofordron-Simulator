using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    public static ScorePlayer Instance;

    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private TMP_Text rongAnswers_Text;
    [SerializeField] private TMP_Text aproveTheoric_Text;

    private Quiz quizManager;
    private int scoreQuiz;
    private int scoreInteractive;

    private int totalScorePractic;
    private int totalScoreQuiz;

    private bool aproveTheoric;
    private bool aprovePractic;

    private int wrongAnswers;

    public void SetScoreQuiz(int scoreQuiz)
    {
        this.scoreQuiz = scoreQuiz;
    }
    public void SetTotalScoreQuiz(int totalScoreQuiz)
    {
        this.totalScoreQuiz = totalScoreQuiz;
    }

    public void SetTotalScorePractic(int totalScorePractic)
    {
        this.totalScorePractic = totalScorePractic;
    }
    public void SetScoreInteractive(int scoreInteractive)
    {
        this.scoreInteractive = scoreInteractive;
    }

    void Start()
    {
        quizManager = GetComponent<Quiz>();
        scoreInteractive = 0;
        scoreQuiz = 0;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("El singleton ScorePlayer ya existe borrando objeto: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void CheckTheoricRequisites()
    {
        int percentageOfTotalScoreQuiz = (scoreQuiz * 100) / totalScoreQuiz;

        if(percentageOfTotalScoreQuiz >= 50)
        {
            aproveTheoric = true;
        }

        wrongAnswers = (totalScoreQuiz / 10) - (scoreQuiz / 10);
    }

    public void CheckPracticRequisites(int wrongA)
    {
        int percentageOfTotalScorePractic = (scoreInteractive * 100) / totalScorePractic;
        if(percentageOfTotalScorePractic >= 50)
        {
            aprovePractic = true;
        }

        wrongAnswers = wrongA;
        Debug.Log(wrongAnswers + "wrongA");
    }

    public void SetInfoTextPractic(TMP_Text score_T, TMP_Text wrongA_T, TMP_Text aproveP, int wrongA) 
    {
        CheckPracticRequisites(wrongA);
        
        if(scoreInteractive < 0)
        {
            scoreInteractive = 0;
        }
        score_T.text = scoreInteractive.ToString();
        wrongA_T.text = wrongAnswers.ToString();

        if (aprovePractic)
        {
            aproveP.text = "COMPLETADO";

        }
        else
        {
            aproveP.text = "NO COMPLETADO";
        }
    }
    public void SetInfoTextTheoric()
    {
        CheckTheoricRequisites();
        //cargo nivel
        LevelLoader.Instance.LoadLevel(quizManager.GetLevelName());

        score_Text.text = scoreQuiz.ToString();
        rongAnswers_Text.text = wrongAnswers.ToString();

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
