using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceFire = 0f;

    [SerializeField] private GameObject bullet = default;
    [SerializeField] private ParticleSystem deathParticle = default;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        // Move to a different script if you want to clean out the shooting mechanics
        timeSinceFire += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (timeSinceFire >= fireRate)
            {
                timeSinceFire = 0f;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
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
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

}
