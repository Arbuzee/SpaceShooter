using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float damage = default;
    [SerializeField] private bool ownedByPlayer = default;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.AddForce(transform.up * 500);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ownedByPlayer && other.CompareTag("Enemy"))
        {
            other.GetComponent<Asteroid>().TakeDamage(damage);
            Death();
        }

        if (!ownedByPlayer && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TakeDamage(damage);
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
