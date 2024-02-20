using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallJson : MonoBehaviour
{
    private static QuestionData questionsData;


    public static QuestionData FindJson(string json)
    {
        
        TextAsset jsonFile = Resources.Load<TextAsset>("QuestionsInJson/" + json);
        if (jsonFile != null)
        {
            // Load JSON content from the TextAsset
            string contenidoJSON = jsonFile.text;
            // Parse JSON content
            questionsData = JsonUtility.FromJson<QuestionData>(contenidoJSON);
            MenuController.instance.StartQuestionMenu(questionsData.questions.Length);
        }
        else
        {
            Debug.LogError("Failed to load JSON file: " + json);
        }
        return questionsData;
    }
}
