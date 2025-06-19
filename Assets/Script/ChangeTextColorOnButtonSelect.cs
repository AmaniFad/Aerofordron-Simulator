using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeTextColorOnButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Selectable button; 
    private TextMeshProUGUI buttonText;
    [SerializeField] private Color unSelectedColor;
    [SerializeField] private Color selectedColor;
    private bool isHovered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>() ;
    }



    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            Selected();
        }
        else if (!isHovered && EventSystem.current.currentSelectedGameObject != this.gameObject)
        {
            UnSelected();
        }

    }

    private void Selected()
    {
        buttonText.color = selectedColor;
    }

    private void UnSelected()
    {
        buttonText.color = unSelectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Selected();
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
