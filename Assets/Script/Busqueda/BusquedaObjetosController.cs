using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusquedaObjetosController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private List<GameObject> spawns = new List<GameObject>();
    [SerializeField] private GameObject objectFind;

    public static BusquedaObjetosController instance;

    [Header("Canvas")]
    [SerializeField] public GameObject canvasNext;
    [SerializeField] public GameObject canvasWin;

    [Header("ObjectsInScene")]
    [SerializeField] private GameObject dronObj;
    [SerializeField] private GameObject spawnDron;

    private int levels;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        objectFind.SetActive(false);
    }
    public void SetObjectInSpawn()
    {
        if (levels < 3)
        {
            if (spawns.Count != 0)
            {
                if (objectFind != null)
                {
                    int randomValue = 0;
                    levels++;
                    if (levels == 1)
                    {
                        randomValue = Random.Range(0, spawns.Count / 3);
                    }
                    else if(levels == 2)
                    {
                        randomValue = Random.Range(0, spawns.Count / 2);
                    }
                    else
                    {
                        randomValue = Random.Range(0, spawns.Count);
                    }
                    
                    Debug.Log(randomValue);
                    objectFind.transform.position = spawns[randomValue].transform.position;
                    objectFind.SetActive(true);
                } 
            }
        }  
    }
    public void ObjectFounded()
    {
        if(levels == 3)
        {
            canvasWin.SetActive(true);
            TimerBusqueda.instance.setTextTime();
        }
        else
        {
            canvasNext.SetActive(true);
            TimerBusqueda.instance.setTextTimeNext();
        }
        TimerBusqueda.instance.SetStartGame(false);
        TimerBusqueda.instance.StopTime();
    }
    public void NextLeve()
    {
        if(levels == 1)
        {
            TimerBusqueda.instance.SetRemainingTime(90);
        }
        if(levels == 2)
        {
            TimerBusqueda.instance.SetRemainingTime(60);
        }

        dronObj.transform.position = spawnDron.transform.position;
        canvasNext.SetActive(false);
        TimerBusqueda.instance.RestartTime();
        TimerBusqueda.instance.StartCountDown();
    }
}