using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronPartFeedback : MonoBehaviour
{
    [SerializeField] private GameObject feedback;
    public void ActivateFeedback()
    {
        Debug.Log("ActivateFeedback");
        feedback.SetActive(true);
    }

    public void DeactivateFeedback()
    {
        Debug.Log("DeactivateFeedback");
        feedback.SetActive(false);
    }
}
