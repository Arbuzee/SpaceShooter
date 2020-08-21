using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject target = default;

    [SerializeField] public float health;
    [SerializeField] private float maxHealth = 100f;

    [Tooltip("Amount of damage to deal when colliding with a player")]
    [SerializeField] private float damage = 10f;

    [SerializeField] private float fireRate = default;
    private float timeSinceFire = 0f;


    [SerializeField] private GameObject bullet = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TakeDamage(damage);
            Debug.Log("Player Hit by asteroid");
        }
    }
    
    void Update()
    {
        if (target != null)
            Rotate();

        timeSinceFire += Time.deltaTime;

        if (timeSinceFire >= fireRate)
        {
            timeSinceFire = 0f;
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(new Vector3(0,0,-90)));
        }
    }

    public void SetPlayer(GameObject player)
    {
        target = player;
    }

    private void Rotate()
    {
        transform.right = target.transform.position - transform.position;
    }
}
