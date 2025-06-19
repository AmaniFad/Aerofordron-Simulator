using UnityEngine;
using UnityEngine.EventSystems;

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
            SendTextToDisplay();
        }
    }

    private void SendTextToDisplay()
    {
        TooltipDisplay.Instance.ChangeTooltipText(tooltip);
    }
}
