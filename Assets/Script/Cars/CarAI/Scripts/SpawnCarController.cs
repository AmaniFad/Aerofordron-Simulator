using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnCarController : MonoBehaviour
{
    public List<CarAI> carList = new List<CarAI>();
    
    [Header("Time to Shoot")]
    [SerializeField] float timeBetween;
    private bool isTime;
    float coolDown = 0;

    [Header("Transform")]
    [SerializeField] private Transform destination;

    void Start()
    {
        isTime = false;
        CarAI[] carIAComponents = GetComponentsInChildren<CarAI>();
        Debug.Log(carIAComponents.Length);
        foreach (CarAI carIA in carIAComponents)
        {
            carList.Add(carIA);
        }
        for (int i = 0; i < carList.Count; i++)
        {
            carList[i].gameObject.SetActive(false);
        }
        respawn();
    }
    void Update()
    {
        coolDown -= Time.deltaTime;
    }
    public void respawn()
    {
        carList[0].gameObject.SetActive(true);
        carList[0].CustomDestination = destination;
        coolDown = timeBetween;
    }
    public void removeCar(CarAI car)
    {
        carList.Remove(car);
    }
    public void AddCar(CarAI car)
    {
        carList.Add(car);
    }
}
