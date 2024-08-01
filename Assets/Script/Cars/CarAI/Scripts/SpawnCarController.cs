using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnCarController : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private List<Transform> destinations = new List<Transform>();

    [SerializeField] private List<CarAI> carList = new List<CarAI>();

    void Start()
    {
        CarAI[] carIAComponents = GetComponentsInChildren<CarAI>();
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
    public void respawn()
    {
        Random rand = new Random();
        int r = rand.Next(0, destinations.Count);

        carList[0].CustomDestination = destinations[r];
        carList[0].gameObject.SetActive(true); 
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
