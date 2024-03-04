using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController instance { get;private set; }
    [SerializeField] private Quiz quiz;
    [SerializeField] GameObject questionButtonContainer;
    private Button[] questionButtons;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite answeredQuestionSprite;
    [SerializeField] Sprite nonAnsweredQuestionSprite;
    private int previousIndex;
    private bool[] answeredState;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Menu Controller ya existe, borrando el objeto: " + gameObject.name);
            Destroy(gameObject);
        }
        questionButtons = questionButtonContainer.GetComponentsInChildren<Button>();
        answeredState = new bool[questionButtons.Length];
        previousIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuestionMenu(int questionAmount)
    {
        RestartQuesionButtons();
        for (int i = 0; i < questionAmount; i++)
        {
            questionButtons[i].gameObject.SetActive(true);
        }
    }

    public void RestartQuesionButtons()
    {
        foreach (var button in questionButtons)
        {
            button.gameObject.SetActive(false);
            button.GetComponent<Image>().sprite = defaultSprite;
        }
    }

    public void CurrentQuestion(int index)
    {
        if (questionButtons[previousIndex].GetComponent<Image>().sprite == nonAnsweredQuestionSprite)
        {
            if (answeredState[previousIndex])
            {
                questionButtons[previousIndex].GetComponent<Image>().sprite = answeredQuestionSprite;
            }
            else
            {
                questionButtons[previousIndex].GetComponent<Image>().sprite = defaultSprite;
            }
        }
        questionButtons[index].GetComponent<Image>().sprite = nonAnsweredQuestionSprite;
        previousIndex = index;

    }
    public void QuestionAnswered(int index)
    {
        answeredState[index]= true ;
        questionButtons[index].GetComponent<Image>().sprite = answeredQuestionSprite;
    }
}
