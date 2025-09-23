using UnityEngine;
using UnityEngine.UI;

public class FogLevelUIManager : MonoBehaviour
{
    [SerializeField] Color unactiveColor;
    [SerializeField] Color activeColor;
    [SerializeField] Image[] fogLevelFeedback;
    
    void Start()
    {
        fogLevelFeedback[MeteoModes.instance.currentFogLevel].color = activeColor;
        SetFeedbackUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFogLevel(int change)
    {
        MeteoModes.instance.ChangeFogLevel(change);
        SetFeedbackUI();

        
    }

    public void SetFeedbackUI()
    {
        for (int i = 0; i< fogLevelFeedback.Length; i++)
        {
            if (i <= MeteoModes.instance.currentFogLevel)
            {

                fogLevelFeedback[i].color = activeColor;
            }
            else
            {

                fogLevelFeedback[i].color = unactiveColor;
            }
        }
    }
}
