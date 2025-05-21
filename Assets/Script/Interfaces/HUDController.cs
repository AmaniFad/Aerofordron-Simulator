using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI speedDisplay;
    [SerializeField] TextMeshProUGUI fpvSpeedDisplay;
    [SerializeField] TextMeshProUGUI heightDisplay;
    [SerializeField] TextMeshProUGUI fpvHeightDisplay;
    private GameObject player;
    private GameObject dron;
    private float speed;

    public float GetSpeed()
    {
        return speed;
    }
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
            speed = Mathf.Floor(dron.GetComponent<Rigidbody>().linearVelocity.magnitude * (60f * 60f) / 1000); // KMH
            speedDisplay.text = speed + " KM/H";
            fpvSpeedDisplay.text = speed + " KM/H";
            if (Mathf.Floor(dron.transform.position.y) < 0)
            {
                heightDisplay.text = 0 + " m";
            }
            else
            { 
                heightDisplay.text = Mathf.Floor(dron.transform.position.y) + " m";
                fpvHeightDisplay.text = Mathf.Floor(dron.transform.position.y) + " m";
            }
            /*else
            { 
                heightDisplay.text = Mathf.Floor(dron.transform.position.y) + " ft";
                fpvHeightDisplay.text = Mathf.Floor(dron.transform.position.y) + " ft";
            }*/
        }
        else
        {
        }
    }
}

