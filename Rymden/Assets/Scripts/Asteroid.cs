using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float health;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem deathParticle;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerVariables>().TakeDamage();
            Debug.Log("Player Hit by asteroid");
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        OnHit();

        if (health <= 0)
        {
            Death();
        }
    }

    private void OnHit()
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
    }

    private void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
