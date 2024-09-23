using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterImpact : MonoBehaviour
{
    public GameObject decalPrefab;  // Prefab del decal que representa el cambio de color
    public float decalDuration = 10.0f;  // Tiempo que dura el decal antes de desaparecer

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            // Obtener el punto de colisión de la partícula
            ParticleSystem ps = other.GetComponent<ParticleSystem>();
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            int numCollisionEvents = ps.GetCollisionEvents(gameObject, collisionEvents);

            for (int i = 0; i < numCollisionEvents; i++)
            {
                // Crear un decal en el punto de colisión
                Vector3 collisionPos = collisionEvents[i].intersection;
                Quaternion rotation = Quaternion.LookRotation(collisionEvents[i].normal);
                GameObject decal = Instantiate(decalPrefab, collisionPos, rotation);

                // Destruir el decal después de cierto tiempo
                Destroy(decal, decalDuration);
            }
        }
    }
}
