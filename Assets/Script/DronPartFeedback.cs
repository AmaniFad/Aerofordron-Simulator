using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronPartFeedback : MonoBehaviour
{
    [SerializeField] private GameObject feedback;
    public void ActivateFeedback()
    {
        feedback.SetActive(true);
    }

    public void DeactivateFeedback()
    {
        feedback.SetActive(false);
    }
}
