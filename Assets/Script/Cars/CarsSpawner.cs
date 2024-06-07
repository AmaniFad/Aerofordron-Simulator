using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CarsSpawner : MonoBehaviour
{
    [Header("SpawnList")]
    [SerializeField] private Transform spawn;

    [Header("Prefab")]
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private GameObject defaultPrefab;
    private GameObject prefab;

    float coolDown = 0;
    [Header("Time to Shoot")]
    [SerializeField] float timeBetween;
    private bool isTime;

    [Header("ConvinationPath")]
    [SerializeField] private List<Transform> pathList1 = new List<Transform>();
    [SerializeField] private List<Transform> pathList2 = new List<Transform>();
    [SerializeField] private List<Transform> pathList3 = new List<Transform>();
    [SerializeField] private List<Transform> pathList4 = new List<Transform>();

    void Start()
    {
        isTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        respawn();
    }

    private void respawn()
    {
        if (!isTime)
        {
            if(coolDown <= 0)
            {
                //elegimos automovil
                if(prefabList.Count > 0)
                {
                    Random rPrefab = new Random();
                    int nPrefab = rPrefab.Next(0, prefabList.Count);
                    prefab = prefabList[nPrefab];
                }
                else
                {
                    prefab = defaultPrefab;
                }

                //instanciamos
                GameObject autoM = Instantiate(prefab);
                autoM.transform.position = spawn.position;
                Random rList = new Random();
                int nList = rList.Next(0, 4);
                switch (nList)
                {
                    case 0:
                        for (int i = 0; i < pathList1.Count; i++)
                        {
                            autoM.GetComponent<CarsController>().AddInList(pathList1[i]);
                        }
                    break;
                    case 1:
                        for (int i = 0; i < pathList2.Count; i++)
                        {
                            autoM.GetComponent<CarsController>().AddInList(pathList2[i]);
                        }
                    break;
                    case 2:
                        for (int i = 0; i < pathList3.Count; i++)
                        {
                            autoM.GetComponent<CarsController>().AddInList(pathList3[i]);
                        }
                    break;
                    case 3:
                        for (int i = 0; i < pathList4.Count; i++)
                        {
                            autoM.GetComponent<CarsController>().AddInList(pathList4[i]);
                        }
                    break;
                }
                coolDown = timeBetween;
                isTime = true;
            }
        }
        else
        {
            isTime = false;
        }
    }
}
