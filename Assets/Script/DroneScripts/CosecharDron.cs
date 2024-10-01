using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosecharDron : MonoBehaviour
{
    [SerializeField] private GameObject agua;
    private bool canShower;
    bool chorro = false;
    private void Start()
    {
        canShower = true;
        agua.SetActive(false);
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
                    print("PRueba");
                    agua.SetActive(true);
                }
                else
                {
                    agua.SetActive(false);
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
