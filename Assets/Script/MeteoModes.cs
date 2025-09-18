using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VInspector;

public class MeteoModes : MonoBehaviour
{
    public static MeteoModes instance;
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

    }

    public void ToggleRain()
    {
        rainParticles.SetActive(true);
        rainParticles.transform.position =Camera.main.transform.position + new Vector3(0, 2, 0);
    }


    [Button]
    public void ToggleClouds()
    {
        VolumetricClouds vol = (VolumetricClouds)clouds.profile.components[0];
        if (vol.cloudPreset == VolumetricClouds.CloudPresets.Stormy)
        vol.cloudPreset = VolumetricClouds.CloudPresets.Sparse;
        else
            vol.cloudPreset = VolumetricClouds.CloudPresets.Stormy;

    }



    [Button]
    public void ToggleFog()
    {
        RenderSettings.fog = !RenderSettings.fog;
    }



    public void AddBlindingSun()
    {

    }

    public void RemoveBlindingSun()
    {

    }
}
