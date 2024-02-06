using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Quiz : MonoBehaviour
{
    private QuestionData questionsData;

    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
    }

    [System.Serializable]
    public class QuestionData
    {
        public Question[] questions;
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetJson(string json)
    {
        string rutaArchivo = Application.dataPath + "/Questions/QuestionsInJson/"+json+".json";
        string contenidoJSON = System.IO.File.ReadAllText(rutaArchivo);
        questionsData = JsonUtility.FromJson<QuestionData>(contenidoJSON);


        for (int i = 0; i < questionsData.questions.Length; i++)
        {
            Debug.Log(questionsData.questions[i].question);
        }
    }
}
