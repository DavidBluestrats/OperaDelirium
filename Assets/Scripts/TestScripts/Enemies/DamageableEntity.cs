using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour
{
    private int maxHealth;
    private int health;

    public void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int _damage)
    {
        health = Mathf.Clamp(health - _damage, 0, maxHealth);
        if (health <= 0) Kill();
    }

    public void Kill()
    {

    }
}
