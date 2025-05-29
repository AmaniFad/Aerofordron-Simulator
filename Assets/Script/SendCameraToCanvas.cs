using UnityEngine;

public class SendCameraToCanvas : MonoBehaviour
{
    [SerializeField] private float cameraDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = cameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
