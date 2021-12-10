using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth = 100;
    private int PrevHealth { get; set; }
    public HealthBar healthBar = null;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        PrevHealth = currentHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (PrevHealth != currentHealth)
        {
            SetHealth(currentHealth);
        }
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        PrevHealth = health;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
