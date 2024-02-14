using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CGAController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string Json;

    [SerializeField] private TMP_Text question;
    [SerializeField] private TMP_Text a1;
    [SerializeField] private TMP_Text a2;
    [SerializeField] private TMP_Text a3;
    [SerializeField] private TMP_Text a4;

    [SerializeField] private TMP_Text score_Text;
    [SerializeField] private TMP_Text rongAnswers_Text;
    [SerializeField] private TMP_Text aproveTheoric_Text;
    private QuestionData questionsData;
    private List<int> answersIndex = new List<int>();
    void Start()
    {
         SetJson(Json);
    }

    public void SetJson(string json)
    {
        questionsData = CallJson.FindJson(json);
    }
    public void setQuestion()
    {
        int i = 4;
        question.text = questionsData.questions[i].question;
        a1.text = questionsData.questions[i].answers[0];
        a2.text = questionsData.questions[i].answers[1];
        a3.text = questionsData.questions[i].answers[2];
        a4.text = questionsData.questions[i].answers[3];
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void AddAnswers(int index)
    {
        answersIndex.Add(index);
    }

    public void CheckAnswer()
    {
        int score = 0;

        for(int i = 1; i < answersIndex.Count; i++)
        {
            if (answersIndex[i] == questionsData.questions[i].correctIndex)
            {
                score += 10;
            }
        }
        int totalScore = questionsData.questions.Length * 10;

        ScorePlayer.Instance.SetTotalScorePractic(totalScore);
        ScorePlayer.Instance.SetScoreInteractive(score);
        ScorePlayer.Instance.SetInfoTextPractic(score_Text, rongAnswers_Text, aproveTheoric_Text);
    }
}
