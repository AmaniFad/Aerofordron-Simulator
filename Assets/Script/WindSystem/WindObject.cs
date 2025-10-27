using UnityEngine;

public class WindObject : MonoBehaviour
{
    //Cuanto le afecta el viento al objeto
    [SerializeField] private float windDampen;
    [SerializeField] private float currentWindDampen;
    [SerializeField] private bool staticOnGround;
    //De momento intentare aplicar el viento con el rigidbody
    Rigidbody rb;
    float windDampening;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetWindDampen(float windDampen)
    {
        this.windDampen = windDampen;
        currentWindDampen = windDampen;
    }
    public float GetWindDampen()
    {
        return currentWindDampen;
    }
    void Start()
    {
        currentWindDampen = windDampen;

        if (TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rb = rigidbody;
            WindManager.instance.SubscribeObject(rigidbody);
        }
        else
        {
            print("No Rigidbody found on object " + gameObject.name);
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (staticOnGround)
        {
            GroundItem();
        }
    }


    private void GroundItem()
    {
        print(Physics.Raycast(transform.position, new Vector3(0, -1, 0)));
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0),5))
        {
            currentWindDampen = 0;
        }
        else
        {
            currentWindDampen = windDampen;
        }

    }
    public void Wind(Vector3 direction, float strength)
    {
        rb.AddForce(direction * strength * currentWindDampen, ForceMode.Acceleration);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position,new Vector3(0,-1,0)));
    }
}
