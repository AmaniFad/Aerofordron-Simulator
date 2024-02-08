using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using TMPro;
using UnityEngine.Purchasing.MiniJSON;
using System.Xml.Schema;
using System.Linq;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text a1Text;
    [SerializeField] private TMP_Text a2Text;
    [SerializeField] private TMP_Text a3Text;
    [SerializeField] private TMP_Text a4Text;

    [SerializeField] private GameObject nextButton;

    private QuestionData questionsData;
    private List<int> answersIndex = new List<int>();
    private bool startGame;

    public void SetStartGameBool(bool startGame)
    {
        this.startGame = startGame;
    }

    void Start()
    {
        startGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            StartGame();
        }
    }

    public void SetJson(string json)
    {
        questionsData = CallJson.FindJson(json);
    }

    public void addAnswerPlayer(int index)
    {
        answersIndex.Add(index);
        startGame = true;
    }

    public void StartGame()
    {
        if(answersIndex.Count < questionsData.questions.Length)
        {
            int i = 0;

            if (answersIndex.Count == 0)
            {
                i = 0;
            }
            else
            {
                i = answersIndex.Count;
            }
            if (questionsData.questions[i].question.Contains(";"))
            {
                string[] subs = questionsData.questions[i].question.Split(';');
                questionText.text = "";
                questionText.alignment = TextAlignmentOptions.Justified;
                foreach (string sub in subs)
                {
                    questionText.text += sub + "\n";
                }
            }
            else
            {
                questionText.alignment = TextAlignmentOptions.Center;
                questionText.text = questionsData.questions[i].question;
            }
            a1Text.text = questionsData.questions[i].answers[0];
            a2Text.text = questionsData.questions[i].answers[1];
            a3Text.text = questionsData.questions[i].answers[2];
            a4Text.text = questionsData.questions[i].answers[3];
        }
        else
        {
            int score = 0;
            for (int i = 0; i < answersIndex.Count; i++)
            {
                if (answersIndex[i] == questionsData.questions[i].correctIndex)
                {
                    score += 10;
                }
            }
            int totalScore = questionsData.questions.Length * 10;
            GetComponent<ScorePlayer>().SetTotalScoreQuiz(totalScore);

            GetComponent<ScorePlayer>().SetScoreQuiz(score);

            nextButton.SetActive(true);
        }
        startGame = false;
    }
}
