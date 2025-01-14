using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneModelContainer : MonoBehaviour
{
    [SerializeField] private GameObject dronModel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDroneModel(GameObject newDrone)
    {
        dronModel = newDrone;
    }

    public GameObject GetDroneModel()
    {
        return dronModel;
    }
}
