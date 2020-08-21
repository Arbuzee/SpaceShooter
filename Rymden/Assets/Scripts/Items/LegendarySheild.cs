using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySheild : MonoBehaviour
{
    [SerializeField] private float takeDamage = 100;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy") )
        {
            Debug.Log("Hit an enemy with shield");
            col.transform.GetComponent<Asteroid>().TakeDamage(takeDamage);
        }
    }
}
