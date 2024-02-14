using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences instance;

    [SerializeField] private GameObject Player;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("El singleton PlayerReferences ya existe borrando objeto: " + gameObject.name);
            Destroy(gameObject);
        }
    }


    public GameObject GetPlayer()
    {
        return Player;
    }
}
