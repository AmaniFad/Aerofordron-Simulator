using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AutoMovement : MonoBehaviour
{
    public Transform[] waypoints; // Lista de puntos a seguir
    public float speed = 2f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de giro
    public float stopDistance = 0.2f; // Distancia mínima para cambiar de waypoint

    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private CharacterController controller;
    private Animator anim;
    private Renderer[] characterMaterials;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();

        // Asegurar que el personaje esté en estado "happy"
        ChangeEyeOffset(EyePosition.happy);
        ChangeAnimatorIdle("happy");
    }

    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Evita que el personaje se mueva en el eje Y

        if (direction.magnitude <= stopDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Ciclo infinito entre waypoints
            return;
        }

        // Rotación suave hacia el siguiente punto
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Movimiento hacia el punto
        controller.Move(direction.normalized * speed * Time.deltaTime);

        // Mantener la animación de caminar feliz
        anim.SetFloat("Blend", 0.25f);
    }

    void ChangeAnimatorIdle(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }

    public enum EyePosition { normal, happy, angry, dead }
}
