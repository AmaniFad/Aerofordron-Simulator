using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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

    int indexArray;
    void Start()
    {
        SetJson(Json);
        indexArray = 0;
    }

    public void SetJson(string json)
    {
        questionsData = CallJson.FindJson(json);
    }
    public void setQuestion()
    {
        int j = 3;
        question.text = questionsData.questions[j].question;
        a1.text = questionsData.questions[j].answers[0];
        a2.text = questionsData.questions[j].answers[1];
        a3.text = questionsData.questions[j].answers[2];
        a4.text = questionsData.questions[j].answers[3];
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void AddAnswers(int index)
    {
        answersIndex.Add(index);
    }
    public void SetIndex(int index)
    {
        indexArray = index;
        Debug.Log(indexArray);
    }
    public void AddAnswerDrop()
    {
        AddAnswers(indexArray);
    }
    public void CheckAnswer()
    {
        int score = 0;
        int wrong = 0;

        for(int i = 0; i < answersIndex.Count; i++)
        {
            if (answersIndex[i] == questionsData.questions[i].correctIndex)
            {
                score += 10;
            }
            else
            {
                score -= 5;

                wrong++;
            }
        }
        int totalScore = questionsData.questions.Length * 10;

        ScorePlayer.Instance.SetTotalScorePractic(totalScore);
        ScorePlayer.Instance.SetScoreInteractive(score);
        ScorePlayer.Instance.SetInfoTextPractic(score_Text, rongAnswers_Text, aproveTheoric_Text, wrong);

        ScorePlayer.Instance.SaveInfo("CGA Pratic", score);
    }

    public void SetCursor(bool visible)
    {
        Cursor.visible = visible;
    }
}
