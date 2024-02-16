using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtenuacionAireController : MonoBehaviour
{
    [SerializeField] TMP_Text infoWarning_Text;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        this.score += score;
    }

    public void ControllerWarning(string infoWarning)
    {
        infoWarning_Text.text = infoWarning;
    }
}
