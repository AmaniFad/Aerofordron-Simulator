using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallJson : MonoBehaviour
{
    private static QuestionData questionsData;

    public static QuestionData FindJson(string json)
    {
        string rutaArchivo = Application.dataPath + "/Questions/QuestionsInJson/" + json + ".json";
        string contenidoJSON = System.IO.File.ReadAllText(rutaArchivo);
        questionsData = JsonUtility.FromJson<QuestionData>(contenidoJSON);
        return questionsData;
    }
}
