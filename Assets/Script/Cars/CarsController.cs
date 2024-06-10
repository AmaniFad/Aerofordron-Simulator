using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private List<Transform> points = new List<Transform>();
    private Vector3 direction;
    private int i;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1.0f;
    private Quaternion targetRotation;

    [Header("Desaceleracion")]
    [SerializeField] private float deceleration;

    [Header("Scripts")]
    private MovementBehaviour MB;

    [Header("RayCast")]
    [SerializeField] private GameObject boxDetect;
    [SerializeField] private Transform OrigenBoxDetect;
    [SerializeField] private int rangRC;
    private RaycastHit hit;
    private GameObject objTouch = null;
    Ray ray;

    void Start()
    {
        MB = GetComponent<MovementBehaviour>();
        targetRotation = transform.rotation;
        i = 0;
    }

    void Update()
    {
        compreobeOthersCars();
    }
    private void FixedUpdate()
    {
        movement();
    }
    public void AddInList(Transform path)
    {
        points.Add(path);
    }
    private void movement()
    {
        if (i < points.Count - 1)
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.04f)
            {
                Debug.Log("Entra " + i);
                i++;
                direction = points[i].position - transform.position;
                /*if (direction != Vector3.zero)
                {
                    targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }*/
            }
        }
        /*if (direction != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }*/
        direction = points[i].position - transform.position;
        MB.MoveRB3D(direction);
    }

    private void compreobeOthersCars()
    {
        if (Physics.Raycast(ray.origin, ray.direction, out hit, rangRC))
        {
            CarsController otherCar = hit.collider.GetComponent<CarsController>();
        
            if (otherCar != null)
            {
                boxDetect.transform.position = hit.collider.transform.position;
                MB.Deceleration(deceleration);
            }
            else
            {
                boxDetect.transform.position = OrigenBoxDetect.position;
            }
        }
        else
        {
            boxDetect.transform.position = OrigenBoxDetect.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rangRC);
        //Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward * rangRC);
    }
}
