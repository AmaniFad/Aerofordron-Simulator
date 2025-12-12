using UnityEngine;
using UnityEngine.UI;
using VInspector;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using System.Collections;

public class ModeHUDFeedbackController : MonoBehaviour
{
    //Importante 0 es tripode, 1 es normal y 2 es sport
    [SerializeField] private TextMeshProUGUI[] modeFeedback;
    private int currentMode;
    [SerializeField] private Animator dronFeedbackAnimator;
    [Foldout("Colors")] 
    [SerializeField] private Color deactivatedColor;
    [SerializeField] private Color activatedColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMode = 1;
        ChangeModeFeedback(currentMode);
    }


    private void Update()
    {
        
    }
    private void OnEnable()
    {
        try
        {

            PlayerReferences.instance.GetDron().GetComponent<DronController>().onDronModeChange += ChangeModeFeedback;
            ChangeModeFeedback(PlayerReferences.instance.GetDron().GetComponent<DronController>().GetCurrentDronMode());
        }
        catch(Exception e)
        {
            print("Could not find Dron reference in player reference, Retrying in 10 seconds");
            StartCoroutine(RetrySubscribingToEvent());
        }
        print("Hola");
    }

    private void OnDisable()
    {
        PlayerReferences.instance.GetDron().GetComponent<DronController>().onDronModeChange -= ChangeModeFeedback;
    }

    private void ChangeModeFeedback(int mode)
    {
        StartCoroutine(ChangeColor(currentMode,mode));
        currentMode = mode;

        if (dronFeedbackAnimator)
        {
            dronFeedbackAnimator.SetInteger("ModeInt",mode);
        }
    }
    private IEnumerator ChangeColor(int old, int newMode)
    {
        modeFeedback[old].color = deactivatedColor;
        yield return new WaitForSeconds(0.1f);
        modeFeedback[newMode].color = activatedColor;
    }
    private IEnumerator RetrySubscribingToEvent()
    {
        yield return new WaitForSeconds(5);
        try
        {

            PlayerReferences.instance.GetDron().GetComponent<DronController>().onDronModeChange += ChangeModeFeedback;
        }
        catch (Exception e)
        {
            print("Could not find Dron reference in player reference");
            StartCoroutine(RetrySubscribingToEvent());
        }
    }
}
