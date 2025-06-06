using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Quaternion rotationOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.parent = Camera.main.transform;
        transform.localRotation = new Quaternion(0,0,0,0) ;
        transform.localPosition = Vector3.zero;
        transform.localPosition = offset;
        transform.localRotation = rotationOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
