using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VInspector;

public class MeteoModes : MonoBehaviour
{

    [System.Serializable]
    private class FogLevels 
    {
        [SerializeField] public float intensity;
        [SerializeField] public float remapMin;
        [SerializeField] public float remapMax;



        
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
    [Tab("Fog")]
    [SerializeField] private UniversalRendererData fogRenderer;
    [SerializeField] private ScriptableRendererFeature fog;
    [SerializeField] private Material fogMaterial;
    [Tab("Rain")]
    [SerializeField] private GameObject rainParticles;
    [SerializeField] private GameObject dronRainParticles;
    [Tab("Clouds")]
    [SerializeField] private UnityEngine.Rendering.Volume clouds;
    [Tab("Night")]
    [SerializeField] private Color nightFogColor;
    [SerializeField] private GameObject nightLight;
    [SerializeField] private Material nightSkybox;
    [Tab("Day")]
    [SerializeField] private Color dayFogColor;
    [SerializeField] private GameObject dayLight;
    [SerializeField] private Material daySkybox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFogLevel = 0;
        foreach ( ScriptableRendererFeature pass in fogRenderer.rendererFeatures)
        {
            if (pass.name == "VolumetricFogRendererFeatureLite")
            {
                print("Hola");
                fog = pass;
            }
        }
        fog.SetActive(false);
        fogMaterial.SetFloat("_Density", fogLevel[currentFogLevel].intensity);
        fogMaterial.SetFloat("_Remap_Min", fogLevel[currentFogLevel].remapMin);
        fogMaterial.SetFloat("_Remap_Max", fogLevel[currentFogLevel].remapMax);
        rainParticles.GetComponent<FollowMeteo>().SetTarget(Camera.main.gameObject);
        dronRainParticles.GetComponent<FollowMeteo>().SetTarget(PlayerReferences.instance.GetDron());
        if (instance == null)
        { 
            instance = this;
            
        }
        else
        {
            print("Ya existe un singleton MeteoModes");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleRain()
    {
        rainParticles.SetActive(!rainParticles.activeInHierarchy);
        dronRainParticles.SetActive(!dronRainParticles.activeInHierarchy);
        rainParticles.transform.position = Camera.main.transform.position + new Vector3(0, 2, 0);
        dronRainParticles.transform.position = PlayerReferences.instance.GetDron().transform.position + new Vector3(0, 2, 0);
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
    public void ToggleFog(bool state)
    {
        fog.SetActive(state);
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
        fogMaterial.SetFloat("_Density", fogLevel[currentFogLevel].intensity);
        fogMaterial.SetFloat("_Remap_Min", fogLevel[currentFogLevel].remapMin);
        fogMaterial.SetFloat("_Remap_Max", fogLevel[currentFogLevel].remapMax);

    }

    public void AddBlindingSun()
    {       


    }

    public void RemoveBlindingSun()
    {

    }

    [Button]
    public void NightMode()
    {
        isDay = false;
        isNight = true;
        nightLight.SetActive(true);
        dayLight.SetActive(false);
        RenderSettings.skybox = nightSkybox;
        RenderSettings.fogColor = nightFogColor;
    }

    [Button]
    public void DayMode()
    {
        isNight = false;
        isDay = true;
        RenderSettings.skybox = daySkybox;
        nightLight.SetActive(false);
        dayLight.SetActive(true);
        RenderSettings.fogColor = dayFogColor;
    }
}
