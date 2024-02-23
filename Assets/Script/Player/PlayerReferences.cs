using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences instance;

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject dronHud;
    private GameObject dron;
    private Rigidbody rb;
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
        rb = Player.GetComponent<Rigidbody>();
    }


    public GameObject GetPlayer()
    {
        return Player;
        
    }

    public Vector3 GetPlayerVelocity()
    {
        return rb.velocity;
    }

    public void SetDron(GameObject dron)
    {
        this.dron = dron;
    }

    public GameObject GetDron()
    {
        return dron;
    }

    public GameObject GetHUD()
    {
        return dronHud;
    }

}
