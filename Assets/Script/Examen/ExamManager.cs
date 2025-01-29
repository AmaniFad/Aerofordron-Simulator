using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject WelcomePanel;
    [SerializeField] private GameObject NextPanel;
    [SerializeField] private List<GameObject> listPanel;
    [SerializeField] private GameObject FadeInPanel;


    private int countLevels;
    void Start()
    {
        countLevels = 0;
        StartCoroutine(CountDown());
    }

    void Update()
    {
        
    }
    //hacer un repit level por si no consigue la prueba en x tiempos
    public void NextLevel()
    {
        FadeInPanel.GetComponent<ScreenFader>().FadeIn();
        
        switch (countLevels)
        {
            case 0:
                listPanel[countLevels].SetActive(true);
                break;
            case 1:
                listPanel[countLevels-1].SetActive(false);
                listPanel[countLevels].SetActive(true);
                break; 
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        FadeInPanel.GetComponent<ScreenFader>().FadeOut();
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2.5f);
        WelcomePanel.SetActive(false);
        NextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        NextPanel.SetActive(true);
    }
}
