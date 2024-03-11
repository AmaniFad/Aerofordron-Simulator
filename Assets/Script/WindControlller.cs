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
    [SerializeField] private GameObject windEffect;

    private float windEffectDuration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_SpawnWindEffects());
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
    private void Update()
    {

    }

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

    private IEnumerator _SpawnWindEffects()
    {
        yield return new WaitForSeconds(Random.Range(3,7));
        //Spawn wind here
        if (leftWind - rightWind != 0 || frontWind - backWind != 0)
        {
            Vector3 windDirection = GetWindForce().normalized;

            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 spawnPosition = cameraPosition + Camera.main.transform.forward * Random.Range(5f, 10f);

            Quaternion rotation = Quaternion.LookRotation(windDirection);
            rotation *= Quaternion.Euler(-90, 0, 0);

            GameObject windInstance = Instantiate(windEffect, spawnPosition, rotation);

            Destroy(windInstance, windEffectDuration);
            Debug.Log("se hace");
        }
        StartCoroutine(_SpawnWindEffects());
    }
}