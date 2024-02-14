using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGAContrller : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private QuestionData questionsData;
    private List<int> answersIndex = new List<int>();
    int i;
    void Start()
    {
        i = 1; 
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
        if (answersIndex.Count < questionsData.questions.Length)
        {

        }
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
