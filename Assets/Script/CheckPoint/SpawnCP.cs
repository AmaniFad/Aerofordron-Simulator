using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnCP : MonoBehaviour
{
    [SerializeField] private bool isAlways;
    [SerializeField] private GameObject cPprefab;

    private void Start()
    {
        RespawnCP();
    }
    public void RespawnCP()
    {
        if (isAlways)
        {
            GameObject cPObj = Instantiate(cPprefab);
            cPObj.transform.position = this.transform.position;
            SpawnCPController.Instance.AddCP(cPObj);
        }
        else
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 3);

            if (randomNumber == 2)
            {
                GameObject cPObj = Instantiate(cPprefab);
                cPObj.transform.position = this.transform.position;
                SpawnCPController.Instance.AddCP(cPObj);
            }
        }
    }
}