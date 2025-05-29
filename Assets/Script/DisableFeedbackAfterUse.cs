using UnityEngine;

public class DisableFeedbackAfterUse : MonoBehaviour
{
    float timer;
    [SerializeField]
    private float treshold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayInputData.isPrimaryPressed)
        {
            if (timer < treshold)
            {

            timer += Time.deltaTime;    
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
