using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowFeedbackStart : MonoBehaviour, IMoveHandler
{
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;


    void IMoveHandler.OnMove(AxisEventData eventData)
    {
        switch (eventData.moveDir)
        {
            case MoveDirection.Left:
                leftArrow.SetActive(false);
                break;
            case MoveDirection.Right:
                rightArrow.SetActive(false);
                break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
       


}
