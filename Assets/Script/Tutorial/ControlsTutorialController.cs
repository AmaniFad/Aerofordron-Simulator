using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VInspector.Libs;
using static Unity.Burst.Intrinsics.X86;

public class ControlsTutorialController : MonoBehaviour
{
    private int currentStep;
    [SerializeField] private Step[] steps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    WriteSlowly writter;
    private bool isInputBeingHeld;
    private int currentActionValue;
    //Esta input action tiene que tener que registre cualquier boton/tecla porque es la encargada de actualizar el esquema de controles de la clase control
    public InputAction checkSchemeOnButtonPress;
    public UnityEvent onTutorialFinish;
    [System.Serializable]
    public class Control
    {
        //Esto es el indicador visual del control para hacer que desaparezca una vez usado
        public GameObject keyboardControl;
        public GameObject gamePadControl;
        public float timeToDisappear;
        private float internalTimer;
        private bool isControlCompleted;
        [SerializeField] private float fadeTime;

        public bool IsCompleted()
        {
            return isControlCompleted;
        }

        public void RunInternalTimer()
        {
            internalTimer += Time.deltaTime;
            CheckIfCompleted();
        }

        public void CheckIfCompleted()
        {
            if (internalTimer > timeToDisappear)
            {
                isControlCompleted = true;
            }
        }

        public void UpdateControlScheme()
        {
            if (isControlCompleted)
                return;
            if (PlayerInputController.Instance.IsUsingGamepad())
            {
                keyboardControl.SetActive(false);
                gamePadControl.SetActive(true);
            }
            else
            {
                keyboardControl.SetActive(true);
                gamePadControl.SetActive(false);

            }
        }

        public void DeactivateText()
        {

            CoroutineManager.instance.StartCoroutine(FadeTextBeforeDeactivate());
        }

        /// <summary>
        /// Makes the text dissapear slowly
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeTextBeforeDeactivate()
        {
            TextMeshProUGUI textKeyboard = keyboardControl.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI textGamepad = gamePadControl.GetComponent<TextMeshProUGUI>();
            float t = 0;
            Color a = textKeyboard.color;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                a.a = Mathf.Lerp(255, 0, 0.1f);
                textKeyboard.color = a;
                textGamepad.color = a;
                yield return null;

            }
            keyboardControl.SetActive(false);
            gamePadControl.SetActive(false);
        }
    }

    #region Step Class
    /// <summary>
    /// Esta clase contiene el texto y objeto necesario para que el paso de tutorial sea entendible, tambien contiene una array llamada controls que permite que puedas hacer que los
    /// controles desaparezcan una vez pulsados por el jugador, para esto el metodo que controla el paso tiene que tener hardcodeado la imagen que tiene que desaparecer cuando pulsas el boton
    /// </summary>
    /// 
    [System.Serializable]
    public class Step
    {
        [SerializeField] private string stepTitle;
        [SerializeField] private TextMeshProUGUI stepTitleDisplay;
        [SerializeField]
        GameObject stepObject;
        [SerializeField] private bool alternativeStepCompletion;

        [SerializeField]
        private float timePressingForActionToCount;
        //Las acciones son todos los botones que debes pulsar antes de que se acabe este paso del tutorial se puede ampliar haciendo que otra posible condicion sea llegar a un sitio
        [SerializeField] private InputAction[] actions;
        [SerializeField] private Control[] controls;
        public GameObject GetStepObject()
        {
            return stepObject;
        }

        public InputAction[] GetStepActions()
        {
            return actions;
        }

        public Control[] GetControlsVisuals()
        {
            return controls;
        }

        public void WriteTextInStep(WriteSlowly writter)
        {
            writter.WriteText(stepTitleDisplay, stepTitleDisplay.text, stepTitleDisplay.text.Length * 0.05f);
            foreach (Control i in controls)
            {
                if (i.keyboardControl)
                {

                    TextMeshProUGUI keyboard = i.keyboardControl.GetComponent<TextMeshProUGUI>();
                    writter.WriteText(keyboard, keyboard.text, keyboard.text.Length * 0.05f);

                }
                if (i.gamePadControl)  
                {

                    TextMeshProUGUI gamepad = i.gamePadControl.GetComponent<TextMeshProUGUI>();
                    writter.WriteText(gamepad, gamepad.text, gamepad.text.Length * 0.05f);

                }

            }
        }
        /// <summary>
        /// Le da un procesador de escala a las actions para que cada uno devuelva cierto indice una vez pulsado 
        /// </summary>
        public void SerializeBindings()
        {
            InputAction[] temp = new InputAction[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                temp[i] = new InputAction(actions[i].name);
                int a = actions[i].bindings.Count;
                for (int y = 0; y < a; y++)
                {
                    temp[i].AddBinding(actions[i].bindings[y].path, " ", "scale(factor=" + i + ")");
                }
            }
            actions = temp;
            foreach (InputAction action in temp)
            {
                action.Dispose();
            }
            ActivateInputs();
            SetStepTitle();
        }

        public void SetStepTitle()
        {
            stepTitleDisplay.text = stepTitle;
        }

        /// <summary>
        /// Desactiva los inputs de los pasos
        /// </summary>
        public void DeactivateInputs()
        {
            foreach (InputAction action in actions)
            {
                action.Disable();
            }
        }

        /// <summary>
        /// Activa los inputs de los pasos
        /// </summary>
        public void ActivateInputs()
        {
            foreach (InputAction action in actions)
            {
                action.Enable();
            }
        }


        /// <summary>
        /// Se encarga de avanzar el reloj interno de cada control para cuando este pulsado ese control
        /// </summary>
        /// <param name="controlIndex">El indice del control que se esta usando</param>
        public void ControlIsPressed(int controlIndex)
        {
            controls[controlIndex].RunInternalTimer();
            print("ControlPressed");
            if (controls[controlIndex].IsCompleted())
            {
                if (PlayerInputController.Instance.IsUsingGamepad())
                {
                    controls[controlIndex].DeactivateText();
                }
                else
                {
                    controls[controlIndex].DeactivateText();
                }
            }
        }

        public bool NeedsAlternativeCondition()
        {
            return alternativeStepCompletion;
        }

    }
    #endregion

    private void Start()
    {
        if (TryGetComponent<WriteSlowly>(out WriteSlowly slowWritter))
        {
            if (slowWritter == null)
            {
                slowWritter = gameObject.AddComponent<WriteSlowly>();

            }
            writter = slowWritter;
        }
        currentStep = 0;
        steps[currentStep].GetStepObject().SetActive(true);
        if (PlayerPrefs.GetInt("doneTutorial") == 1)
        {
            this.enabled = false;
        }
        foreach (Step step in steps)
        {
            step.SerializeBindings();
        }
        foreach (InputAction action in steps[currentStep].GetStepActions())
        {
            action.Enable();
            action.performed += InputIsPressed;
            action.canceled += InputIsNotPressed;
        }
        steps[0].WriteTextInStep(writter);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (CheckStep() && currentStep < steps.Length - 1)
        {
            StartNextStep();
        }
        else if (CheckStep() && currentStep >= steps.Length - 1)
        {
            onTutorialFinish.Invoke();
            this.enabled = false;
        }
        if (isInputBeingHeld)
        {
            CheckControls();
        }
    }



    private void StartNextStep()
    {
        try
        {
            Step oldStep = steps[currentStep];
            oldStep.GetStepObject().SetActive(false);
            oldStep.DeactivateInputs();
        }
        catch (Exception e)
        {
            Debug.Log("Error while disposing of old Step: " + e);
        }

        currentStep++;
        try
        {
            Step newStep = steps[currentStep];
            newStep.GetStepObject().SetActive(true);
            newStep.SerializeBindings();
            newStep.WriteTextInStep(writter);
            foreach (InputAction action in newStep.GetStepActions())
            {
                action.Enable();
                action.performed += InputIsPressed;
                action.canceled += InputIsNotPressed;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error while initializing new Step: " + e);
        }
    }

    public void EndAlternativeConditionStep(int stepIndex)
    {
        if (stepIndex == currentStep && steps[currentStep].NeedsAlternativeCondition())
        {
            StartNextStep();
        }
    }

    private bool CheckStep()
    {
        Step step = steps[currentStep];
        bool aux = false;

        if (!step.NeedsAlternativeCondition())
        {
            Control[] controls = steps[currentStep].GetControlsVisuals();
            int i = 0;
            foreach (Control control in controls)
            {
                if (control.IsCompleted())
                {
                    i++;
                }
            }
            if (i == controls.Length)
                aux = true;

        }

        return aux;
    }

    private void CheckControls()
    {
        steps[currentStep].ControlIsPressed(currentActionValue);
    }

    private void InputIsPressed(InputAction.CallbackContext context)
    {
        isInputBeingHeld = true;
        currentActionValue = (Mathf.RoundToInt(context.ReadValue<float>()));
    }

    private void InputIsNotPressed(InputAction.CallbackContext context)
    {
        isInputBeingHeld = false;
    }

    private void CheckControlSchemeOnButtonPress(InputAction.CallbackContext ctx)
    {

        Control[] controls = steps[currentStep].GetControlsVisuals();
        foreach (Control control in controls)
        {
            control.UpdateControlScheme();
        }
    }

    private void OnEnable()
    {
        checkSchemeOnButtonPress.Enable();
        checkSchemeOnButtonPress.performed += CheckControlSchemeOnButtonPress;

    }

    private void OnDisable()
    {
        checkSchemeOnButtonPress.Disable();
        checkSchemeOnButtonPress.performed -= CheckControlSchemeOnButtonPress;
    }
}
