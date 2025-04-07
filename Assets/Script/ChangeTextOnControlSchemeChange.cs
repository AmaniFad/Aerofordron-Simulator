using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextOnControlSchemeChange : MonoBehaviour
{
    [SerializeField]
    private string keyboardText;
    [SerializeField]
    private string gamepadText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputController.Instance.IsUsingGamepad())
        {
            GetComponent<TextMeshProUGUI>().text = gamepadText;
        }
        else
        {

            GetComponent<TextMeshProUGUI>().text = keyboardText;
        }
    }
}
