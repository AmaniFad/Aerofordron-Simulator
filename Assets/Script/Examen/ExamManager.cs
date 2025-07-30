using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExamManager : MonoBehaviour
{
    #region variables
    [Header("Canvas")]
    [SerializeField] private List<GameObject> listPanel;
    [SerializeField] private List<GameObject> buttonPanelSelect;
    [SerializeField] private GameObject FadeInPanel;
    [SerializeField] private GameObject panelAdvertecia;
    [SerializeField] private TMP_Text numEjercice;

    [Header("GameObjectsLevels")]
    [SerializeField] private List<GameObject> pointsDetector;
    [SerializeField] private GameObject detectorPoint301;
    [SerializeField] private GameObject mando;
    [SerializeField] private GameObject dronHud;

    [Header("Player & Dron")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dron;
    [SerializeField] private GameObject DronAutoLevel8;
    [SerializeField] private GameObject PersonaLevel9;

    [Header("Scripts")]
    [SerializeField] private HUDController HUDController;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera dronCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public int _countLevels;
    private int _countLine, _finalPoint;
    private bool _is4Level;
    private bool _isDronAuto8;
    private bool _isPersonaAuto;

    private bool onLevel;
    private float timeInPanel;
    #endregion
    #region getters and setters
    public void SetIsLevel(bool is4Level)
    {
        this._is4Level = is4Level;
    }
    public void SetIsDronAuto8(bool isDronAuto8)
    {
        this._isDronAuto8 = isDronAuto8;
    }
    public void SetIsPerson9(bool _isPersonaAuto)
    {
        this._isPersonaAuto = _isPersonaAuto;
    }
    #endregion
    void Start()
    {
        if (PlayerPrefs.GetInt("SceneInitialized", 0) == 0)
        {
            // Primera vez en la escena
            for (int i = 0; i < buttonPanelSelect.Count; i++)
            {
                buttonPanelSelect[i].GetComponent<Image>().color = Color.green;

                // También guarda el estado como "no completado"
                string key = "BotonNivel_" + i;
                PlayerPrefs.SetInt(key, 0);
            }

            // Marcar como ya inicializada para la próxima vez
            PlayerPrefs.SetInt("SceneInitialized", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Ya has entrado antes: carga colores desde PlayerPrefs
            for (int i = 0; i < buttonPanelSelect.Count; i++)
            {
                string key = "BotonNivel_" + i;
                int state = PlayerPrefs.GetInt(key, 0);

                if (state == 0)
                    buttonPanelSelect[i].GetComponent<Image>().color = Color.green;
                else if (state == 1)
                    buttonPanelSelect[i].GetComponent<Image>().color = Color.white;
            }
        }
        //countLevels = 0;
        //StartCoroutine(_CountDown());  
    }

    void Update()
    {
        if (_is4Level)
        {
            if(HUDController.GetSpeed() > 18)
            {
                ReturnToStart();
            }
        }
        if (_isDronAuto8)
        {
            if (Vector3.Distance(DronAutoLevel8.transform.position, Dron.transform.position) > 15f)
            {
                panelAdvertecia.SetActive(true);
                /*timeInPanel += Time.deltaTime;

                if(timeInPanel > 10)
                {
                    //reset level
                    DronAutoLevel8.SetActive(false);
                    TimerExamen.Instance.SetTime(false);
                    TimerExamen.Instance.remainingTime = 45;
                    Dron.GetComponent<DroneCrash>().GoToFirstPosition();
                    NextLevel();
                    timeInPanel = 0;
                    panelAdvertecia.SetActive(false);

                }*/
            }
            else
            {
                timeInPanel = 0;
                panelAdvertecia.SetActive(false);
            }
        } 
        else if (_isPersonaAuto)
        {
            if (Vector3.Distance(PersonaLevel9.transform.position, Dron.transform.position) > 10f)
            {
                panelAdvertecia.SetActive(true);
            }
            else
            {
                panelAdvertecia.SetActive(false);
            }
        }
        if (onLevel)
        {
            if (Player.GetComponent<PlayerInteract>().GetCurrentFeedback() != null)
            {
                if (Player.GetComponent<PlayerInteract>().GetCurrentFeedback().activeSelf)
                {
                    listPanel[_countLevels].SetActive(false);
                    numEjercice.gameObject.SetActive(false);
                }
                else if (dronHud.activeSelf)
                {
                    listPanel[_countLevels].SetActive(false);
                    numEjercice.gameObject.SetActive(false);
                }
                else
                {
                    listPanel[_countLevels].SetActive(true);
                    numEjercice.gameObject.SetActive(true);
                }
            }
            
        }
        
    }
    public void NextLevel()
    {
        onLevel = true;
        listPanel[_countLevels].SetActive(true);
        numEjercice.gameObject.SetActive(true);
        numEjercice.text = (_countLevels + 1).ToString();
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
            case 8:
                calculeDistancePoint(30f, 50f, pointsDetector[_countLevels]);
                break;
            default:
                break;
        }
    }
    public void SetTheLevel(int level)
    {
        listPanel[_countLevels].SetActive(false);
        numEjercice.gameObject.SetActive(false);
        pointsDetector[_countLevels].SetActive(false);

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

        //FadeInPanel.GetComponent<ScreenFader>().FadeOutCoroutine();
    }
    IEnumerator _ChangePanels()
    {
        yield return new WaitForSeconds(2);
        listPanel[_countLevels].SetActive(false);
        onLevel = false;
        Dron.SetActive(true);
        
        dronCamera.LookAt = Dron.transform;
        Dron.GetComponent<DroneCrash>().GoToFirstPosition();

        yield return new WaitForSeconds(1);

        buttonPanelSelect[_countLevels].GetComponent<Image>().color = Color.white;
        string key = "BotonNivel_" + _countLevels;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        virtualCamera.Priority = 20;
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
