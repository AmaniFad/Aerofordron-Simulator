using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0f;
    }


    public void CheckPointPassed()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
