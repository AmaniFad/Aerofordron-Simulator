using TMPro;
using UnityEngine;

public class TooltipDisplay : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    public static TooltipDisplay Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTooltipText(string tooltipText)
    {
        textDisplay.text = tooltipText; 
    }
}
