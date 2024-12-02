using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorGrass : MonoBehaviour
{
    public Color startColor = Color.yellow;  
    public Color endColor = Color.green;     
    public float colorChangeSpeed = 0.5f;    

    public Color currentColor;
    private float t = 0f;
    private bool hasChanged = false;

    void Start()
    {
        if (ColorChangeManager.instance != null)
        {
            ColorChangeManager.instance.RegisterObject();
        }

        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            Material instancedMaterial = rend.material; 
            instancedMaterial.color = startColor;  
        }

        currentColor = startColor;
    }

    void OnParticleCollision(GameObject other)
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            Material instancedMaterial = rend.material;
            if (instancedMaterial.color != endColor && !hasChanged)
            {
                currentColor = Color.Lerp(currentColor, endColor, t);
                instancedMaterial.color = currentColor;

                t += Time.deltaTime * colorChangeSpeed;

                if (t > 1f)
                {
                    t = 1f;
                }

                if (instancedMaterial.color == endColor && !hasChanged)
                {
                    hasChanged = true;  
                    if (ColorChangeManager.instance != null)
                    {
                        ColorChangeManager.instance.ObjectColorChanged();
                    }
                }
            }
        }
    }
}
