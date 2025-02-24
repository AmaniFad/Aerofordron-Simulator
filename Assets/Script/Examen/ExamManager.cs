using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;

    [Header("Scripts")]
    [SerializeField] private HUDController HUDController;

    public int countLevels;
    private int countLine, finalPoint;
    private bool is4Level;

    public void SetLevel(bool is4Level)
    {
        this.is4Level = is4Level;
    }
    void Start()
    {
        //countLevels = 0;
        //StartCoroutine(_CountDown());  
    }

    void Update()
    {
        if (is4Level)
        {
            if(HUDController.GetSpeed() > 18)
            {
                NextLevel();
            }
        }
    }
    public void NextLevel()
    {
        switch (countLevels)
        {
            case 0:
                listPanel[countLevels].SetActive(true);
                calculeDistancePoint(5f, 1.5f, pointsDetector[countLevels]);
            break;
            case 1:
                returnToStart();

                calculeDistancePoint(5f, 20f, pointsDetector[countLevels]);
                countLine = 0;
                finalPoint = 8;
            break; 
            case 2:
                returnToStart();
                calculeDistancePoint(40f, 40f, pointsDetector[countLevels]);
                calculeDistancePoint(7f, 20f, detectorPoint301);
                detectorPoint301.SetActive(false);
            break;
            case 3:
                returnToStart();
                calculeDistancePoint(5f, 30f, pointsDetector[countLevels]);
                
                countLine = 0;
                finalPoint = 5;
            break;
            case 4:
                returnToStart();
                calculeDistancePoint(5f, 50f, pointsDetector[countLevels]);
                break;

            default:
                break;
        }
    }
    IEnumerator _CountDown()
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
    public void SetTheLevel(int level)
    {
        countLevels = level;
        NextLevel();
    }

    private void calculeDistancePoint(float distanceX, float distanceY, GameObject point)
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

        StartCoroutine(_ChangePanels());

        FadeInPanel.GetComponent<ScreenFader>().FadeOutCoroutine();
    }
     
    IEnumerator _ChangePanels()
    {
        yield return new WaitForSeconds(2);
        listPanel[countLevels - 1].SetActive(false);
        listPanel[countLevels].SetActive(true);

        Dron.GetComponent<DroneCrash>().GoToFirstPosition();
    }
    public void AddPointsLine()
    {
        countLine++;
        Debug.Log(countLine);
    }
    public void ComprobePointLine()
    {
        if(countLine == finalPoint)
        {
            AddCount();
            NextLevel();
        }
        else
        {
            NextLevel();
        }
    }

    public void PointToDron(Transform point)
    {
        point.transform.position = Dron.transform.position;
        is4Level = true;
    }
    public void IsDronGounded()
    {
        if (!Dron.GetComponent<DroneCrash>().GetIsCrashed())
        {
            Debug.Log("aterriza");
        }
    }
}
