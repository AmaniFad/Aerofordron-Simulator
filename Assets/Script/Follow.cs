using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.TransformPoint(offset);
        transform.rotation = target.transform.rotation;
    }


    public void SetTarget(GameObject target)
    {
        this.target = target;
    }


}
