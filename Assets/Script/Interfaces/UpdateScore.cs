using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    private Slider slider;
    public void UpdateCanvas(int h)
    {
        GetComponent<TextMeshProUGUI>().text = h.ToString();
    }
    
    public void UpdateCanvasSlider(int value)
    {
        if (slider = GetComponent<Slider>())
        {
            slider.value = value;
        }
    }
   
}
