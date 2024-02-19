using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtenuacionAireController : MonoBehaviour
{
    [SerializeField] TMP_Text infoWarning_Text;

    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private TMP_Text rongAnswers_Text;
    [SerializeField] private TMP_Text aproveTheoric_Text;
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
        if(score == -5)
        {
            wrong++;
        }
        this.score += score;
        totalScore++;
    }

    public void ControllerWarning(string infoWarning)
    {
        infoWarning_Text.text = infoWarning;
    }
    
    public void CheckAnswer()
    {
        ScorePlayer.Instance.SetTotalScorePractic(totalScore);
        ScorePlayer.Instance.SetScoreInteractive(score);
        ScorePlayer.Instance.SetInfoTextPractic(score_Text, rongAnswers_Text, aproveTheoric_Text, wrong);

        ScorePlayer.Instance.SaveInfo("Atenuacion en Aire Pratic", score);
    }

}
