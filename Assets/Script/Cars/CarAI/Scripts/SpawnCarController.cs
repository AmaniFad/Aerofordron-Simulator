using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnCarController : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private GameObject defaultPrefab;
    private GameObject prefab;

    float coolDown = 0;
    [Header("Time to Shoot")]
    [SerializeField] float timeBetween;
    private bool isTime;

    [Header("Spawns")]
    [SerializeField] private List<Transform> spawnList = new List<Transform>();

    void Start()
    {
        isTime = false;
    }
    void Update()
    {
        coolDown -= Time.deltaTime;
        respawn();
    }
    private void respawn()
    {
        if (!isTime)
        {
            if (coolDown <= 0)
            {
                //elegimos automovil
                if (prefabList.Count > 0)
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
                Debug.Log("Instanciamos");
                GameObject autoM = Instantiate(prefab);
                autoM.transform.position = this.gameObject.transform.position;
                autoM.transform.rotation = Quaternion.identity;

                // elegimos destino
                Random rList = new Random();
                int nList = rList.Next(0, spawnList.Count);
                autoM.GetComponent<CarAI>().CustomDestination = spawnList[nList];

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
