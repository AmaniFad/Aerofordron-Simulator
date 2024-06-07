using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField] private bool changeDirection;

    public bool GetChangeDirection()
    {
        return changeDirection;
    }
}
