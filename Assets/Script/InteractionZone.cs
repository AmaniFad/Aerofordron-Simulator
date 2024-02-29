using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public static InteractionZone Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("El Singleton InteractionZone ya existe, borrando el objeto: " + this.gameObject.name);
            Destroy(gameObject);
        }
    }

    public Transform GetInteractionZone()
    {
        return transform;
    }
}
