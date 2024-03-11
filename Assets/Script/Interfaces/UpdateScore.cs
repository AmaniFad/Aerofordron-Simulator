using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public void UpdateCanvas(int h)
    {
        GetComponent<TextMeshProUGUI>().text = h.ToString();
    }
}
