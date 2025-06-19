using System.Collections;
using TMPro;
using UnityEngine;

public class ControlsTutorial : MonoBehaviour
{
    private int frameCounter;
    private bool movedUpOrDown;
    private bool hasRotated;
    private Coroutine writeCoroutine;
    [SerializeField] private int currentTutorialStage;
    [SerializeField] private string[] stagesTextController;
    [SerializeField] private string[] stagesTextKeyboard;
    [SerializeField] private TextMeshProUGUI displayText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movedUpOrDown = false;
        hasRotated = false;
        frameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {


        frameCounter++;
        if (frameCounter == 10)
        {
            frameCounter = 0;
            StageController();
        }

    }

    private void StageController()
    {
        if (currentTutorialStage == 1)
        {
            SecondTestComprobation();
        }
        if (currentTutorialStage == 2)
        {
            ThirdStageComprobation();
        }
        if (PlayerInputController.Instance.IsUsingGamepad())
        {
            if (writeCoroutine == null)
            {

                writeCoroutine = StartCoroutine(WriteSlowly(stagesTextController[currentTutorialStage]));
            }
        }
        else
        {
            if (writeCoroutine == null)
            {

                writeCoroutine = StartCoroutine(WriteSlowly(stagesTextKeyboard[currentTutorialStage]));
            }
        }
    }


    public void PassStage()
    {
        currentTutorialStage++;
    }

    public void PassFirstStage()
    {
        if (currentTutorialStage == 0)
        {
            currentTutorialStage++;
        }
    }

    private void SecondTestComprobation()
    {
        if (DronInputController.Instance.GetVerticalInput() != 0)
        {
            movedUpOrDown = true;
        }
        if (DronInputController.Instance.GetRotationalInput() != 0)
        {
            hasRotated = true;
        }
        if (movedUpOrDown && hasRotated)
        {
            PassStage();
        }
    }

    private void ThirdStageComprobation()
    {
        if (DronInputController.Instance.GetDirectionInput().x > 0)
        {
            StartCoroutine(PassStageWithCooldown());
        }
    }

    private IEnumerator PassStageWithCooldown()
    {
        yield return new WaitForSeconds(1);
        PassStage();
    }


    private IEnumerator WriteSlowly(string textToDisplay)
    {
        displayText.text = "";
        float t = 0;
        int i = 0;
        float totalTime = textToDisplay.Length * 0.05f;
        while (t < totalTime)
        {

            displayText.text = displayText.text + textToDisplay.Substring(i, 1);
            yield return new WaitForSeconds(0.05f);
            i++;
            t += Time.deltaTime;
        }
        writeCoroutine = null;
    }
}
