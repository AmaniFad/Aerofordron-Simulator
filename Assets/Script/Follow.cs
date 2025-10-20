using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool notChangeRotation;
    [SerializeField] private bool onlyFirstFrame;
    // Start is called before the first frame update
    void Start()
    {
        if (onlyFirstFrame)
        {
            transform.position = target.transform.TransformPoint(offset);
            if (!notChangeRotation)
                transform.rotation = target.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!onlyFirstFrame)
        {
            transform.position = target.transform.TransformPoint(offset);
            if (!notChangeRotation)
                transform.rotation = target.transform.rotation;
        }

    }


    public void SetTarget(GameObject target)
    {
        this.target = target;
    }


}
