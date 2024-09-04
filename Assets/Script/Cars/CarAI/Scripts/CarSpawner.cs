using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] carPrefabs; 
    void Start()
    {
        Instantiate(SelectACarPrefab(), transform);
    }
    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0,carPrefabs.Length);
        return carPrefabs[randomIndex];
    }
}
