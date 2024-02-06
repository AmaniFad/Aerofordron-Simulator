using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private float batteryTime;
    //Esto es para poder calcular el porcentaje o poder recargar la bateria
    private float maxBatteryTime;

    public static BatteryController Instance { get; private set; }


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Ya existe un BatteryController, recuerda poner los singletons en el GameObject Manager en la escena");
        }
        maxBatteryTime = batteryTime;
    }

    void Update()
    {
        batteryTime -= Time.deltaTime; 
    }

    public bool HasBattery()
    {
        if (batteryTime > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float BatteryAmount()
    {
        return batteryTime;
    }

    public float MaxBatteryAmount()
    {
        return maxBatteryTime;
    }
}
