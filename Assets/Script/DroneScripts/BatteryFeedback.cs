using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BatteryFeedback : MonoBehaviour
{

    [SerializeField] private Image batteryBar;
    [SerializeField] private TextMeshProUGUI batteryPercentage;
    private float maxBattery;

    private void OnEnable()
    {
        //Hago esto para ahorrar llamadas a la clase BatteryController
    }
    // Aqui voy actualizando la barra, hace falta que cuando te quedes sin bateria apareca algo que te avise
    void Update()
    {
        maxBattery = BatteryController.Instance.MaxBatteryAmount();
        if (BatteryController.Instance.HasBattery())
        {
            float percentage = Mathf.Floor((BatteryController.Instance.BatteryAmount() / maxBattery) * 100 );
            batteryPercentage.text = percentage.ToString() + "%" ;
            batteryBar.fillAmount = percentage/100;
        }
        else
        {
            batteryBar.fillAmount = 0;
            Debug.Log("Ran Out of battery");
        }
    }
}
