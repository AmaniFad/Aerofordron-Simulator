using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{

    [SerializeField]
    private GameObject forestCamera;
    [SerializeField]
    private GameObject mainButtons;
    [SerializeField]
    private GameObject flyButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToFlySelection()
    {
        mainButtons.SetActive(false);
        flyButtons.SetActive(true);
    }

    public void ChangeToMainSelection()
    {
        mainButtons.SetActive(true);
        flyButtons.SetActive(false);
    }
    public void ChangeToFlySelectionCamera()
    {

        ChangeToFlySelection();
    }

    public void ChangeToMainCamera()
    {

        ChangeToMainSelection();

    }
}
