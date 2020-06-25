using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recieve_Damage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    void Start()
    {
        health = maxHealth;
        
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }
    void CheckOverHeal()
    {
        if(health> maxHealth)
        {
            health = maxHealth;
        }
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
