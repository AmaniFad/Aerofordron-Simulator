using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtMovement : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + rb.linearVelocity);
    }
}
