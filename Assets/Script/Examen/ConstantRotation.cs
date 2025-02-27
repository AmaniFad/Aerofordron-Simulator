using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float velocityRotate;
  
    void Update()
    {
        transform.Rotate(Vector3.down * velocityRotate * Time.deltaTime);
    }
}
