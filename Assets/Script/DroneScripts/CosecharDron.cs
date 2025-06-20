using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CosecharDron : MonoBehaviour
{
    [SerializeField] private float maxWaterAmount;
    private float currentWaterAmount;
    [SerializeField] private GameObject[] agua;
    [SerializeField] private Image waterAmountImage;
    [SerializeField] private TextMeshProUGUI waterAmountPercentage;
    private void Start()
    {
        currentWaterAmount = maxWaterAmount;
        foreach (GameObject go in agua)
        {
            go.SetActive(false);
        }
    }
    void Update()
    {
        SoltarAgua();
        waterAmountImage.fillAmount = currentWaterAmount / maxWaterAmount;
        waterAmountPercentage.text = (Mathf.Round((currentWaterAmount / maxWaterAmount) * 100) * 10) / 10 + "%";
    }
    public void SoltarAgua()
    {
        float chorro = DronInputController.Instance.GetAguaInput();
        if (chorro == 1)
        {
            currentWaterAmount -= Time.deltaTime;
            foreach (GameObject go in agua)
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in agua)
            {
                go.SetActive(false);
            }
        }
    }

}
