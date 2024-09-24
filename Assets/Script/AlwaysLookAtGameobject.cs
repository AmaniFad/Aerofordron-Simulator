using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AlwaysLookAtGameobject : MonoBehaviour
{
    [SerializeField] private GameObject lookAtObject;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool constraintX;
    [SerializeField] private bool constraintY;
    [SerializeField] private bool constraintZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtObject)
        {

            transform.LookAt(lookAtObject.transform);
            transform.Rotate(rotationOffset);
            Vector3 currentEulerAngles = transform.rotation.eulerAngles;

            // Apply constraints
            if (constraintX) currentEulerAngles.x = transform.rotation.eulerAngles.x;
            if (constraintY) currentEulerAngles.y = transform.rotation.eulerAngles.y;
            if (constraintZ) currentEulerAngles.z = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(currentEulerAngles);

        }
        else
            Debug.Log("Falta asignarle un objeto al script AlwaysLookAtGameobject en " + gameObject.name);
    }

    public void SetObjective(GameObject objective)
    {
        lookAtObject = objective;
    }

    public void SetOffset(Vector3 offset)
    {
        rotationOffset = offset;
    }

    public void ConstraintX(bool constraint)
    {
        constraintX = constraint;
    }
    public void ConstraintY(bool constraint)
    {
        constraintX = constraint;
    }
    public void ConstraintZ(bool constraint)
    {
        constraintX = constraint;
    }

}
