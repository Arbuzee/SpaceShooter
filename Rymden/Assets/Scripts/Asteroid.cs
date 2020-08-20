using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private float health;
    [SerializeField] private float maxHealth = 100f;

    [Tooltip("Amount of damage to deal when colliding with a player")]
    [SerializeField] private float damage = 10f;

    [SerializeField] private ParticleSystem hitParticle = default;
    [SerializeField] private ParticleSystem deathParticle = default;

    [SerializeField] private GameObject asteroid = default;
    [Tooltip("0-Big, 1-Medium, 2-Small || Let the script know whether it should spawn more asteroids on death")] [Range(0,2)]
    [SerializeField] private int asteroidSize = default;

    [SerializeField] private float minVelocity = default;
    [SerializeField] private float maxVelocity = default;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxHealth;

        SetRandomVelocity(Random.Range(minVelocity, maxVelocity));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TakeDamage(damage);
            Debug.Log("Player Hit by asteroid");
        }
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

        if (asteroidSize == 0)
            for (int i = 0; i < 2; i++)
            {
                Instantiate(asteroid, transform.position, Quaternion.identity);
            }
        else if (asteroidSize == 1)
            for (int i = 0; i < 4; i++)
            {
                Instantiate(asteroid, transform.position, Quaternion.identity);
            }

        Destroy(gameObject);
    }
}
