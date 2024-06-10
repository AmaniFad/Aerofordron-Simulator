using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerFromLeavingScenario : MonoBehaviour
{
    [SerializeField] private GameObject boundaryWarning;
    void Start()
    {
        foreach (LimitDetector limit in GetComponentsInChildren<LimitDetector>())
        {
            limit.SetLimitController(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EnteredBoundary()
    {
        if (boundaryWarning)
        {
            boundaryWarning.SetActive(true);
        }
    }

    public void LeftBoundary()
    {
        if (boundaryWarning)
        {
            boundaryWarning.SetActive(false);
        }
    }

    public void ReachedLimits(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<DroneCrash>(out DroneCrash drone))
        {
            drone.Respawn();
        }
    }

}
