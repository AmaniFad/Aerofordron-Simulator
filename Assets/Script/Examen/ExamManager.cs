using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject SelectLevel;
    [SerializeField] private List<GameObject> listPanel;
    [SerializeField] private GameObject FadeInPanel;

    [Header("GameObjectsLevels")]
    [SerializeField] private List<GameObject> pointsDetector;
    [SerializeField] private GameObject detectorPoint301;

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;
    [SerializeField] private GameObject DronAuto;

    [Header("Scripts")]
    [SerializeField] private HUDController HUDController;

    public int countLevels;
    private int countLine, finalPoint;
    private bool is4Level;
    private bool isDronAuto;

    public void SetIsLevel(bool is4Level)
    {
        this.is4Level = is4Level;
    }
    public void SetIsDronAuto(bool isDronAuto)
    {
        this.isDronAuto = isDronAuto;
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
        if (isDronAuto)
        {
            if (DronInputController.Instance.GetRemoteDron())
            {
                Dron.SetActive(false);
                DronAuto.transform.position = Dron.transform.position;
                DronAuto.SetActive(true);
                
                isDronAuto = false;
            }
        }
    }
    public void NextLevel()
    {
        listPanel[countLevels].SetActive(true);
        switch (countLevels)
        {
            case 0:
                calculeDistancePoint(5f, 1.5f, pointsDetector[countLevels]);
            break;
            case 1:

                calculeDistancePoint(5f, 20f, pointsDetector[countLevels]);
                countLine = 0;
                finalPoint = 8;
            break; 
            case 2:

                calculeDistancePoint(40f, 40f, pointsDetector[countLevels]);
                calculeDistancePoint(7f, 20f, detectorPoint301);
                detectorPoint301.SetActive(false);
            break;
            case 3:

                calculeDistancePoint(5f, 30f, pointsDetector[countLevels]);
                
                countLine = 0;
                finalPoint = 5;
            break;
            case 4:

                calculeDistancePoint(5f, 50f, pointsDetector[countLevels]);
            break;
            case 5:
                calculeDistancePoint(30f, 50f, pointsDetector[countLevels]);
                break;
            case 6:
                calculeDistancePoint(100f, 50f, pointsDetector[countLevels]);
                break;


            default:
                break;
        }
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
    public void ReturnToStart()
    {
        FadeInPanel.SetActive(true);
        FadeInPanel.GetComponent<ScreenFader>().FadeInCoroutine();

        StartCoroutine(_ChangePanels());

        FadeInPanel.GetComponent<ScreenFader>().FadeOutCoroutine();
    }
    IEnumerator _ChangePanels()
    {
        yield return new WaitForSeconds(2);
        listPanel[countLevels].SetActive(false);
        Dron.GetComponent<DroneCrash>().GoToFirstPosition();

        yield return new WaitForSeconds(1);
        SelectLevel.SetActive(true);
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
            ReturnToStart();
        }
        else
        {
            Dron.GetComponent<DroneCrash>().GoToFirstPosition();
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
            ReturnToStart();
        }
    }
}
