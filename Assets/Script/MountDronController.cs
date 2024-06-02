using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountDronController : MonoBehaviour
{
    public static MountDronController instance;
    [SerializeField] private GameObject[] instructionList;
    private int currentInstruction;
    void Start()
    {
        instance = this;
        currentInstruction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextInstruction()
    {
        if (currentInstruction + 1 < instructionList.Length)
        {
            instructionList[currentInstruction].SetActive(false);
            currentInstruction++;
            instructionList[currentInstruction].SetActive(true);
        }
    }
}
