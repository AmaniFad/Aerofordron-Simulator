using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private bool firstRound;
    
    public static TutorialController instance;

    public void SetFirstRound(bool firstRound)
    {
        this.firstRound = firstRound;
    }
    public bool GetFirstRound()
    {
        return this.firstRound;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
