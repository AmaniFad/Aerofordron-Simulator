using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CGAContrller : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string Json;

    [SerializeField] private TMP_Text question;
    [SerializeField] private TMP_Text a1;
    [SerializeField] private TMP_Text a2;
    [SerializeField] private TMP_Text a3;
    [SerializeField] private TMP_Text a4;
    private QuestionData questionsData;
    private List<int> answersIndex = new List<int>();
    int i;
    void Start()
    {
         SetJson(Json);
    }

    // Update is called once per frame
    void Update()
    {
        

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
}
