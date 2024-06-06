using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CarsSpawner : MonoBehaviour
{
    [Header("SpawnList")]
    [SerializeField] private List<Transform> spawnList = new List<Transform>();
    [SerializeField] private Transform defaultSpawn;
    private Transform spawn;

    [Header("Prefab")]
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private GameObject defaultPrefab;
    private GameObject prefab;

    [Header("Time to Shoot")]
    float coolDown = 0;
    [SerializeField] float timeBetween;
    private bool isTime;

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
                //elegimos sapwn
                if(spawnList.Count > 0)
                {
                    Random rSpawn = new Random();
                    int nSpawn = rSpawn.Next(0, spawnList.Count);
                    spawn = spawnList[nSpawn];
                }
                else
                {
                    spawn = defaultSpawn;
                }
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
