using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color startColor = Color.yellow;  // Color inicial (Amarillo)
    public Color endColor = Color.green;     // Color final (Verde)
    public float colorChangeSpeed = 0.5f;    // Velocidad de cambio

    private Color currentColor;
    private float t = 0f;

    void Start()
    {
        // Si el objeto tiene hijos con materiales, los inicializamos con el color de inicio
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            Material instancedMaterial = rend.material; // Instancia el material para evitar cambiar todos
            instancedMaterial.color = startColor;  // Color inicial (amarillo)
        }

        currentColor = startColor;
    }

    // Este método se llama cuando una partícula colisiona con el objeto (o sus hijos)
    void OnParticleCollision(GameObject other)
    {
        // Obtener todos los renderers en los hijos
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            // Instanciar (clonar) el material de cada hijo si no lo hemos hecho ya
            Material instancedMaterial = rend.material;

            // Lerp (Interpolación) entre el color actual y el color final (verde)
            currentColor = Color.Lerp(currentColor, endColor, t);

            // Aplicamos el nuevo color al material instanciado
            instancedMaterial.color = currentColor;

            // Incrementamos el valor de "t" para que el cambio sea gradual
            t += Time.deltaTime * colorChangeSpeed;

            // Nos aseguramos de que t no sobrepase 1, lo que indica que el color final es verde
            if (t > 1f)
            {
                t = 1f;
            }
        }
    }
}
