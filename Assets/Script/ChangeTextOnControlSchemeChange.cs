using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextOnControlSchemeChange : MonoBehaviour
{
    [SerializeField]
    private GameObject keyboardText;
    [SerializeField]
    private GameObject gamepadText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputController.Instance.IsUsingGamepad())
        {
            gamepadText.SetActive(true);
            keyboardText.SetActive(false);
            //GetComponent<TextMeshProUGUI>().text = gamepadText;
        }
        else
        {
            gamepadText.SetActive(false);
            keyboardText.SetActive(true);
            //GetComponent<TextMeshProUGUI>().text = keyboardText;
        }
    }
}
