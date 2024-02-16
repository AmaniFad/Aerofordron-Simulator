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
    [Header("Quiz UI")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text a1Text;
    [SerializeField] private TMP_Text a2Text;
    [SerializeField] private TMP_Text a3Text;
    [SerializeField] private TMP_Text a4Text;

    [SerializeField] private GameObject nextButton;

    private QuestionData questionsData;
    private int[] answersIndex;
    private int currentIndex;
    private string levelName;

    #region Classes
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctIndex;
    }

    [System.Serializable]
    public class QuestionData
    {
        public Question[] questions;
    }
    #endregion


    #region Metodos
    public void SetJson(string json)
    {
        string rutaArchivo = Application.dataPath + "/Questions/QuestionsInJson/" + json + ".json";
        string contenidoJSON = System.IO.File.ReadAllText(rutaArchivo);
        questionsData = JsonUtility.FromJson<QuestionData>(contenidoJSON);
        answersIndex = new int[questionsData.questions.Length];
        MenuController.instance.StartQuestionMenu(questionsData.questions.Length);
    }

    public void AddAnswerPlayer(int index)
    {
        if (index < questionsData.questions.Length)
        {
            answersIndex[currentIndex] = index;
            MenuController.instance.QuestionAnswered(currentIndex);

        }
    }




    public void LoadQuestion()
    {
        if (currentIndex < questionsData.questions.Length)
        {
            MenuController.instance.CurrentQuestion(currentIndex);
            if (questionsData.questions[currentIndex].question.Contains(";"))
            {
                string[] subs = questionsData.questions[currentIndex].question.Split(';');
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
                questionText.text = questionsData.questions[currentIndex].question;
            }
            a1Text.text = questionsData.questions[currentIndex].answers[0];
            a2Text.text = questionsData.questions[currentIndex].answers[1];
            a3Text.text = questionsData.questions[currentIndex].answers[2];
            a4Text.text = questionsData.questions[currentIndex].answers[3];
        }
        else
        {
            CheckScore();
        }
    }
    public void NextQuestion()
    {
        if (currentIndex < questionsData.questions.Length)
        {
            currentIndex++;
        }
        LoadQuestion();
    }
    public void PrevQuestion()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }

        LoadQuestion();
    }

    public void CheckScore()
    {
        int score = 0;
        for (int i = 0; i < answersIndex.Length; i++)
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

    public void RestartQuiz()
    {
        questionsData.questions = null;
        answersIndex = null;
    }

    public int GetQuestionAmount()
    {
        return questionsData.questions.Length;
    }

    public void GoToQuestion(int question)
    {
        currentIndex = question;
        LoadQuestion();
    }

    public void SetLevel(string sceneName)
    {
        levelName = sceneName;
    }

    public string GetLevelName()
    {
        return levelName;
    }
    #endregion
}
