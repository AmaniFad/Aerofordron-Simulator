using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectFirstButton : MonoBehaviour
{

    public void Select()
    {
        // Find all buttons among the children of this GameObject
        Button[] buttons = GetComponentsInChildren<Button>();

        // Loop through all buttons to find the first active one
        foreach (Button button in buttons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
                // Select the first active button

                return; // Stop the loop after selecting the first active button
            }
        }
    }


    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Select();
        }
    }
}