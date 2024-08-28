using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusquedaObjetosController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawns = new List<GameObject>();
    [SerializeField] private GameObject objectFind;

    public static BusquedaObjetosController instance;

    [SerializeField] public GameObject canvasNext;
    [SerializeField] public GameObject canvasWin;

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
        if (levels == 3)
        {
            if (spawns.Count != 0)
            {
                if (objectFind != null)
                {
                    int randomValue = Random.Range(0, spawns.Count + 1);
                    objectFind.transform.position = spawns[randomValue].transform.position;
                    levels++;
                } 
            }
        }
        
    }
    public void ObjectFounded()
    {
        if(levels == 3)
        {
            canvasWin.SetActive(false);
            TimerBusqueda.instance.StopTime();
        }
        else
        {
            canvasNext.SetActive(true);
            TimerBusqueda.instance.StopTime();
        }
    }
}
