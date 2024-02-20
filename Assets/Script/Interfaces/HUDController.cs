using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI speedDisplay;
    [SerializeField] TextMeshProUGUI heightDisplay;
    private GameObject player;
    private GameObject dron;

    void Start()
    {
        Debug.developerConsoleVisible = true;
        Debug.developerConsoleEnabled = true;
        player = PlayerReferences.instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        dron = PlayerReferences.instance.GetDron();

        if (dron != null)
        {
            float speed = Mathf.Floor(dron.GetComponent<Rigidbody>().velocity.magnitude * (60f * 60f) / 1000); // KMH
            speedDisplay.text = speed + " KM/H";
            heightDisplay.text = Mathf.Floor(dron.transform.position.y) + " ft";
        }
        else
        {
        }
    }
}
