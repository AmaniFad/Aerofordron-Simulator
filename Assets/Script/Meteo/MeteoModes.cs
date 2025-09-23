using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using VInspector;

public class MeteoModes : MonoBehaviour
{

    [System.Serializable]
    private class FogLevels 
    {
        [SerializeField] public FogMode fog_mode;
        [SerializeField] public float fog_density;



        
    }
    public static MeteoModes instance;
    //This bools are for debugging;
    [SerializeField] private bool isRaining;
    [SerializeField] private bool isSunny;
    [SerializeField] private bool isFoggy;
    [SerializeField] private bool isCloudy;
    [SerializeField] private bool isDay;
    [SerializeField] private bool isNight;
    [SerializeField] private FogLevels[] fogLevel;
    [SerializeField]
    public int currentFogLevel;
    [Tab("Rain")]
    [SerializeField] private GameObject rainParticles;
    [Tab("Clouds")]
    [SerializeField] private UnityEngine.Rendering.Volume clouds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else
        {
            print("Ya existe un singleton MeteoModes");
        }
        currentFogLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleRain()
    {
        rainParticles.SetActive(!rainParticles.activeInHierarchy);
        rainParticles.transform.position = Camera.main.transform.position + new Vector3(0, 2, 0);
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
        RenderSettings.fogDensity = fogLevel[currentFogLevel].fog_density;
        RenderSettings.fogMode = fogLevel[currentFogLevel].fog_mode;
    }

    //Changes fog level according to parameter
    public void ChangeFogLevel(int fogChangeIndex)
    {
        if (currentFogLevel + fogChangeIndex >= fogLevel.Length)
        {
            currentFogLevel = 0;
        }
        else if (currentFogLevel + fogChangeIndex < 0)
        {
            currentFogLevel = fogLevel.Length - 1;
        }
        else
        {
            currentFogLevel += fogChangeIndex;
        }
        
        RenderSettings.fogDensity = fogLevel[currentFogLevel].fog_density;
        RenderSettings.fogMode = fogLevel[currentFogLevel].fog_mode;
    }

    public void AddBlindingSun()
    {

    }

    public void RemoveBlindingSun()
    {

    }
}
