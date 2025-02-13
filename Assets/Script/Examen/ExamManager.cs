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

    [Header("GameObjectsLevels")]
    [SerializeField] private GameObject detectorLevel1;
    [SerializeField] private GameObject detectorLevel2;

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;

    private int countLevels;
    private int countLine;
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
        switch (countLevels)
        {
            case 0:
                listPanel[countLevels].SetActive(true);
                CalculeDistancePoint(5f, 2f, detectorLevel1);
            break;
            case 1:
                returToStart();

                CalculeDistancePoint(5f, 20f, detectorLevel2);
            break; 
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(2.5f);
        WelcomePanel.SetActive(false);
        NextPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        NextPanel.GetComponent<Animator>().SetBool("ZoomOut", true);
        yield return new WaitForSeconds(0.6f);
        NextPanel.SetActive(false);
    }

    public void AddCount()
    {
        countLevels++;
    }

    private void CalculeDistancePoint(float distanceX, float distanceY, GameObject point)
    {
        Vector3 positionPlayer = Player.transform.position;
        Vector3 escalaPlayer = Player.transform.localScale;

        Vector3 newPositionX = Player.transform.forward * distanceX * escalaPlayer.x;
        Vector3 newPositionY = Player.transform.up * distanceY;

        point.SetActive(true);
        point.transform.position = positionPlayer + newPositionX + newPositionY;
    }
    private void returToStart()
    {
        FadeInPanel.SetActive(true);
        FadeInPanel.GetComponent<ScreenFader>().FadeInCoroutine();

        StartCoroutine(ChangePanels());

        FadeInPanel.GetComponent<ScreenFader>().FadeOutCoroutine();
    }
     
    IEnumerator ChangePanels()
    {
        yield return new WaitForSeconds(2);
        listPanel[countLevels - 1].SetActive(false);
        listPanel[countLevels].SetActive(true);

        Dron.GetComponent<DroneCrash>().GoToFirstPosition();
    }
    public void addPointsLine()
    {
        countLine++;
        Debug.Log(countLine);

        if(countLine == 4)
        {
            AddCount();
            NextLevel();
        }
    }

}
