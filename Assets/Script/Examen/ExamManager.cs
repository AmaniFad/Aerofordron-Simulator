using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ExamManager : MonoBehaviour
{
    #region variables
    [Header("Canvas")]
    [SerializeField] private GameObject SelectLevel;
    [SerializeField] private List<GameObject> listPanel;
    [SerializeField] private GameObject FadeInPanel;
    [SerializeField] private GameObject panelAdvertecia;

    [Header("GameObjectsLevels")]
    [SerializeField] private List<GameObject> pointsDetector;
    [SerializeField] private GameObject detectorPoint301;
    [SerializeField] private GameObject mando;

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;
    [SerializeField] private GameObject DronAutoLevel7;
    [SerializeField] private GameObject DronAutoLevel8;

    [Header("Scripts")]
    [SerializeField] private HUDController HUDController;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera dronCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public int _countLevels;
    private int _countLine, _finalPoint;
    private bool _is4Level;
    private bool _isDronAuto7;
    private bool _isDronAuto8;

    private float timeInPanel;
    #endregion
    #region getters and setters
    public void SetIsLevel(bool is4Level)
    {
        this._is4Level = is4Level;
    }
    public void SetIsDronAuto(bool isDronAuto)
    {
        this._isDronAuto7 = isDronAuto;
    }
    #endregion
    void Start()
    {
        //countLevels = 0;
        //StartCoroutine(_CountDown());  
    }

    void Update()
    {
        if (_is4Level)
        {
            if(HUDController.GetSpeed() > 18)
            {
                NextLevel();
            }
        }
        if (_isDronAuto7)
        {
            if (DronInputController.Instance.GetRemoteDron())
            {
                Dron.SetActive(false);
                DronAutoLevel7.transform.position = Dron.transform.position;
                DronAutoLevel7.SetActive(true);
                dronCamera.LookAt = DronAutoLevel7.transform;
                _isDronAuto7 = false;
            }
        }
        if (_isDronAuto8)
        {
            if(Vector3.Distance(DronAutoLevel8.transform.position, Dron.transform.position) > 10f)
            {
                panelAdvertecia.SetActive(true);
                timeInPanel = Time.deltaTime;

                if(timeInPanel > 4)
                {
                    //reset level
                }
            }
            else
            {
                timeInPanel = 0;
                panelAdvertecia.SetActive(false);
            }
        }
    }
    public void NextLevel()
    {
        listPanel[_countLevels].SetActive(true);
        switch (_countLevels)
        {
            case 0:
                calculeDistancePoint(5f, 1.5f, pointsDetector[_countLevels]);
            break;
            case 1:

                calculeDistancePoint(5f, 20f, pointsDetector[_countLevels]);
                _countLine = 0;
                _finalPoint = 8;
            break; 
            case 2:

                calculeDistancePoint(40f, 40f, pointsDetector[_countLevels]);
                calculeDistancePoint(7f, 20f, detectorPoint301);
                detectorPoint301.SetActive(false);
            break;
            case 3:

                calculeDistancePoint(5f, 30f, pointsDetector[_countLevels]);
                
                _countLine = 0;
                _finalPoint = 5;
            break;
            case 4:

                calculeDistancePoint(5f, 50f, pointsDetector[_countLevels]);
            break;
            case 5:
                calculeDistancePoint(30f, 50f, pointsDetector[_countLevels]);
                break;
            case 6:
                calculeDistancePoint(100f, 50f, pointsDetector[_countLevels]);
                break;
            case 7:
                calculeDistancePoint(20f,50f, pointsDetector[_countLevels]);
                break;
            default:
                break;
        }
    }
    public void SetTheLevel(int level)
    {
        _countLevels = level;
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
        listPanel[_countLevels].SetActive(false);
        Dron.SetActive(true);
        
        dronCamera.LookAt = Dron.transform;
        Dron.GetComponent<DroneCrash>().GoToFirstPosition();

        yield return new WaitForSeconds(1);
        SelectLevel.SetActive(true);
    }
    public void AddPointsLine()
    {
        _countLine++;
    }
    public void ComprobePointLine()
    {
        if(_countLine == _finalPoint)
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
        _is4Level = true;
    }
    public void IsDronGounded()
    {
        if (!Dron.GetComponent<DroneCrash>().GetIsCrashed())
        {
            ReturnToStart();
        }
    }

    public void FrameVirtualCamera()
    {
        virtualCamera.gameObject.SetActive(true);
        mando.SetActive(false);
        StartCoroutine(_IsCamera());
    }
    IEnumerator _IsCamera()
    {
        yield return new WaitForSeconds(5);
        virtualCamera.gameObject.SetActive(false);
        mando.SetActive(true);
    }
}
