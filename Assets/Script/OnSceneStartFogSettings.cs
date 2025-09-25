using UnityEngine;
using UnityEngine.Rendering.Universal;
using VInspector;

public class OnSceneStartFogSettings : MonoBehaviour
{
    [Tab("FogReference")]
    [SerializeField] private UniversalRendererData fogRenderer;
    [SerializeField] private ScriptableRendererFeature fog;
    [SerializeField] private Material fogMaterial;
    [Tab("FogSettings")]
    [SerializeField] private bool fogEnabled;
    [SerializeField] private float intensity;
    [SerializeField] private float remapMin;
    [SerializeField] private float remapMax;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (ScriptableRendererFeature pass in fogRenderer.rendererFeatures)
        {
            if (pass.name == "VolumetricFogRendererFeatureLite")
            {
                print("Hola");
                fog = pass;
            }
        }
        fog.SetActive(fogEnabled);
        fogMaterial.SetFloat("_Density", intensity);
        fogMaterial.SetFloat("_Remap_Min", remapMin);
        fogMaterial.SetFloat("_Remap_Max", remapMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
