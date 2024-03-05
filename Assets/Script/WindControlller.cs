using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControlller : MonoBehaviour
{
    public static WindControlller Instance;
    [SerializeField] private float leftWind;
    [SerializeField] private float rightWind;
    [SerializeField] private float frontWind;
    [SerializeField] private float backWind;
    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Ya existe una instancia de wind controller destruyendo objeto: " + gameObject.name);
        }
    }

    // Update is called once per frame


    public void SetWind(float frontWind, float backWind, float rightWind, float leftWind)
    {
        this.frontWind = frontWind;
        this.backWind = backWind;
        this.rightWind = rightWind;
        this.leftWind = leftWind;
    }

    public void SetFrondWind(float fronWind)
    {
        this.frontWind = fronWind;
    }

    public void SetBackWind(float backWind)
    {
        this.backWind = backWind;
    }

    public void SetLeftWind(float leftWind)
    {
        this.leftWind = leftWind;
    }

    public void SetRightWind(float rightWind)
    {
        this.rightWind = rightWind;
    }
    public Vector3 GetWindForce()
    {
        return new Vector3(rightWind - leftWind, 0f, frontWind - backWind);
    }
}
