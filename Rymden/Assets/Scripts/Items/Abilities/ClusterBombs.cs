using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombs : MonoBehaviour
{
    [SerializeField] float _takeDamage = 100;
    [SerializeField] float _projectileSpeed = 1;

    [SerializeField] List<GameObject> _target;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && _target[0] == null)
        {
            _target[0] = other.gameObject;
        }
    }

    void Update()
    {
        SpawnBomb();
    }

    void SpawnBomb()
    {
        if (_target[0] != null)
        {
            _projectileSpeed += 0.05f;
            float _dist = Vector3.Distance(transform.position, _target[0].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _target[0].transform.position, _projectileSpeed * Time.deltaTime);

            if (_dist == 0)
            {
                _target[0].GetComponent<Asteroid>().TakeDamage(_takeDamage);
                Destroy(transform.gameObject);
            }
        }
    }
}
