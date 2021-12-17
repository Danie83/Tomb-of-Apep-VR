using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int trapDamage = 10;
    public int maxHealth = 100;
    public int currentHealth = 100;
    private int PrevHealth { get; set; }
    public HealthBar healthBar = null;

    private float damageCountdown;
    [SerializeField]
    private float defaultDamageCountdown = 1;

    public UnityEvent playerIsDead;
    public int restartScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageCountdown = 0;
        currentHealth = maxHealth;
        PrevHealth = currentHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Trap"))
        {
            damageCountdown -= Time.deltaTime;
            if (damageCountdown <= 0)
            {
                TakeDamage(trapDamage);
                damageCountdown = defaultDamageCountdown;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PrevHealth != currentHealth)
        {
            SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            playerIsDead.Invoke();
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
