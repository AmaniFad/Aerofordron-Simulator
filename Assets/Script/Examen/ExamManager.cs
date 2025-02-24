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
    [SerializeField] private List<GameObject> pointsDetector;
    [SerializeField] private GameObject detectorPoint301;
    [SerializeField] private GameObject rectangleLevel4;

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;

    [Header("Scripts")]
    [SerializeField] private HUDController HUDController;

    private int countLevels;
    private int countLine, finalPoint;
    private bool is4Level;

    public void SetLevel(bool is4Level)
    {
        this.is4Level = is4Level;
    }
    void Start()
    {
        countLevels = 0;
        StartCoroutine(CountDown());  
    }

    void Update()
    {
        if (is4Level)
        {
            if(HUDController.GetSpeed() > 5)
            {
                returnToStart();
            }
        }
    }
    public void NextLevel()
    {
        switch (countLevels)
        {
            case 0:
                listPanel[countLevels].SetActive(true);
                CalculeDistancePoint(5f, 1.5f, pointsDetector[countLevels]);
            break;
            case 1:
                returnToStart();

                CalculeDistancePoint(5f, 20f, pointsDetector[countLevels]);
                finalPoint = 8;
            break; 
            case 2:
                returnToStart();
                CalculeDistancePoint(40f, 40f, pointsDetector[countLevels]);
                CalculeDistancePoint(7f, 20f, detectorPoint301);
                detectorPoint301.SetActive(false);
            break;
            case 3:
                returnToStart();
                CalculeDistancePoint(5f, 30f, pointsDetector[countLevels]);
                rectangleLevel4.transform.position = Dron.transform.position;
                countLine = 0;
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
    private void returnToStart()
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

        if(countLine == finalPoint)
        {
            AddCount();
            NextLevel();
        }
    }

}
