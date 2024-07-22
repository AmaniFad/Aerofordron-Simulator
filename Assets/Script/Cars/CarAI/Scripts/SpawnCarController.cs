using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnCarController : MonoBehaviour
{
    [Header("Prefab")]
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
        respawn();
        coolDown -= Time.deltaTime;
    }
    private void respawn()
    {
        if (!isTime)
        {
            if (coolDown <= 0)
            {
                //elegimos automovil

                //instanciamos
                Debug.Log("Instanciamos");
                GameObject autoM = Instantiate(defaultPrefab);
                autoM.transform.position = this.gameObject.transform.position;
                Debug.Log(this.gameObject);

                // elegimos destino
                if(spawnList.Count > 0)
                {
                    Random rList = new Random();
                    int nList = rList.Next(0, spawnList.Count);
                    autoM.GetComponent<CarAI>().CustomDestination = spawnList[nList];
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
