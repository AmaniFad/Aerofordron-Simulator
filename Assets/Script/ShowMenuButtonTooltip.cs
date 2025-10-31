using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class ShowMenuButtonTooltip : MonoBehaviour
{
    [SerializeField] private string tooltip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            SendTextToDisplay(tooltip);
        }
    }

    private void SendTextToDisplay(string text)
    {
        TooltipDisplay.Instance.ChangeTooltipText(text);
    }

    public void SetTooltip(string tooltip)
    {
        this.tooltip = tooltip;
    }
}
