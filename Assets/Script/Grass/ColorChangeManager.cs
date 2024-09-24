using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeManager : MonoBehaviour
{
    public static ColorChangeManager instance;

    private int totalObjects;
    private int changedObjects;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private GameObject finalCanvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalObjects = 0;
        changedObjects = 0;

        UpdateProgressText();
    }

    public void RegisterObject()
    {
        totalObjects++;
        UpdateProgressText();
    }

    public void ObjectColorChanged()
    {
        changedObjects++;
        UpdateProgressText();

        if (changedObjects >= totalObjects)
        {
            finalCanvas.SetActive(true);
            
            
        }
    }

    // Método para actualizar el progreso en la UI
    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            float progressPercentage = ((float)changedObjects / totalObjects) * 100f;
            progressText.text =  progressPercentage.ToString("F2") + "%";
        }
    }
}
