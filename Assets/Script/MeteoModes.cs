using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VInspector;

public class MeteoModes : MonoBehaviour
{
    //This bools are for debugging;
    [SerializeField] private bool isRaining;
    [SerializeField] private bool isSunny;
    [SerializeField] private bool isFoggy;
    [SerializeField] private bool isCloudy;
    [SerializeField] private bool isDay;
    [SerializeField] private bool isNight;

    [Tab("Rain")]
    [SerializeField] private GameObject rainParticles;
    [Tab("Clouds")]
    [SerializeField] private UnityEngine.Rendering.Volume clouds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaining)
        {
            SetRain();
        }
        else
        {
            StopRain();
        }
    }

    public void SetRain()
    {
        rainParticles.SetActive(true);
        rainParticles.transform.position =Camera.main.transform.position + new Vector3(0, 2, 0);
    }

    public void StopRain()
    {
        rainParticles.SetActive(false);

    }

    [Button]
    public void AddClouds()
    {
        //print(clouds.profile.components[0].parameters.);
        
        VolumetricClouds vol = (VolumetricClouds)clouds.profile.components[0];
        vol.cloudPreset = VolumetricClouds.CloudPresets.Stormy;
    }


    public void RemoveClouds()
    {
        VolumetricClouds vol = (VolumetricClouds)clouds.profile.components[0];
        vol.cloudPreset = VolumetricClouds.CloudPresets.Sparse;
    }

    [Button]
    public void AddFod()
    {
        RenderSettings.fog = true;
    }

    public void RemoveFog()
    {
        RenderSettings.fog = false;
    }
}
