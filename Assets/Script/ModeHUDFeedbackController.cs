using UnityEngine;
using UnityEngine.UI;
using VInspector;
using TMPro;


public class ModeHUDFeedbackController : MonoBehaviour
{
    //Importante 0 es tripode, 1 es normal y 2 es sport
    [SerializeField] private TextMeshProUGUI[] modeFeedback;
    private int currentMode;

    [Foldout("Colors")] 
    [SerializeField] private Color deactivatedColor;
    [SerializeField] private Color activatedColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMode = 1;
    }



    private void OnEnable()
    {
        PlayerReferences.instance.GetDron().GetComponent<DronController>().onDronModeChange += ChangeModeFeedback;
    }

    private void OnDisable()
    {
        PlayerReferences.instance.GetDron().GetComponent<DronController>().onDronModeChange -= ChangeModeFeedback;
    }

    private void ChangeModeFeedback(int mode)
    {
        modeFeedback[currentMode].color = deactivatedColor;
        currentMode = mode;
        modeFeedback[currentMode].color = activatedColor;
    }
}
