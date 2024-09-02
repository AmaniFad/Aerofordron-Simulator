using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    [Header("Events")]
    public UnityEvent OnDie;
    public UnityEvent OnDamage;

    public UnityEvent<int> updateCanvaHealth;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
    public void Start()
    {
        ResetHealth();
    }
    public void Damage(int damage)
    {
        currentHealth -= damage;

        OnDamage.Invoke();
        updateCanvaHealth.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            updateCanvaHealth.Invoke(currentHealth);
            OnDie.Invoke();
        }
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        updateCanvaHealth.Invoke(currentHealth);
    }
}
