using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakeTextAppearOnScreen : MonoBehaviour
{
    [SerializeField] private string textToPut;
    [SerializeField] private Color textColor;
    [SerializeField] private int textSize;
    [SerializeField] private bool autoSize;
    [SerializeField] private TMP_FontAsset textFont;
    [SerializeField] private TextAlignmentOptions aligment;
    [SerializeField] private bool appearOnCollision;
    [SerializeField] private Vector2 minAnchor;
    [SerializeField] private Vector2 maxAnchor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateText()
    {
        GameObject canvas = Instantiate(new GameObject());

        canvas.AddComponent<Canvas>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.AddComponent<GraphicRaycaster>();
        GameObject text = Instantiate(new GameObject(), canvas.transform);
        text.AddComponent<RectTransform>();
        text.GetComponent<RectTransform>().anchorMin = minAnchor;
        text.GetComponent<RectTransform>().anchorMax = maxAnchor;
        text.AddComponent<CanvasRenderer>();
        text.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI textComponent = text.GetComponent<TextMeshProUGUI>();
        textComponent.text = textToPut;
        textComponent.color = textColor;
        textComponent.autoSizeTextContainer = autoSize;
        textComponent.fontSize = textSize;
        textComponent.alignment = aligment;
        textComponent.font = textFont;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (appearOnCollision)
        {
            CreateText();
        }
    }


        
        
}
