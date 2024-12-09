using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosecharDron : MonoBehaviour
{
    [SerializeField] private GameObject[] agua;
    private bool canShower;
    bool chorro = false;
    private void Start()
    {
        canShower = true;
        foreach (GameObject i in agua)
        {
            i.SetActive(false);
        }
    }
    void Update()
    {
        SoltarAgua();
    }
    public void SoltarAgua()
    {
        if (canShower)
        {
            if (DisplayInputData.isPrimaryPressed)
            {
                StartCoroutine(Wait());
                chorro = !chorro;
                if (chorro)
                {
                    foreach (GameObject i in agua)
                    {
                        i.SetActive(true);
                    }
                }
                else
                {
                    foreach (GameObject i in agua)
                    {
                        i.SetActive(false);
                    }
                }
            }

        }

    }


    private IEnumerator Wait()
    {
        canShower = false;
        yield return new WaitForSeconds(0.3f);
        canShower = true;
    }
}
