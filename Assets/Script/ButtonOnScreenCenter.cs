using UnityEngine;
using UnityEngine.UI;

public class ButtonOnScreenCenter : MonoBehaviour
{
    // The layer mask used for raycasting (set in Unity editor)
    public LayerMask buttonLayerMask;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the center of the screen
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            // Perform the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buttonLayerMask))
            {
                // Check if the raycast hits a button
                Button button = hit.collider.GetComponent<Button>();
                if (button != null)
                {
                    // Invoke the button's click event
                    button.onClick.Invoke();
                }
            }
        }
    }
}