using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private float health;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private ParticleSystem hitParticle = default;
    [SerializeField] private ParticleSystem deathParticle = default;

    [SerializeField] private GameObject asteroid = default;
    [SerializeField] private bool spawnOnDeath = default;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

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

    public void SetVelocity()
    {
        rb2d.AddForce(new Vector2(0, 500));
    }

    public void SetRandomVelocity(float range)
    {
        rb2d.AddForce(new Vector2(Random.Range(-range, range), Random.Range(-range, range)));
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

        if (spawnOnDeath)
            Instantiate(asteroid, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
