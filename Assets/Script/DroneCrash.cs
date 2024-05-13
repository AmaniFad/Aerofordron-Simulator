using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCrash : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Limits"))
        {
            Respawn();
        }
    }


    private void Respawn()
    {
        gameObject.transform.position = spawnPoint.position;
    }
}
