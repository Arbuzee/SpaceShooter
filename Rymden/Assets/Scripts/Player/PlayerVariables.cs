using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = default;

    private void Start()
    {
        health = maxHealth;
    }

    private void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {

    }

}
