using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = default;
    [SerializeField] private GameObject asteroid = default;

    [SerializeField] private float spawnRadius = default;

    private float timeSinceSpawn = 0f;

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= spawnRate)
        {
            timeSinceSpawn = 0;

            GameObject go = Instantiate(asteroid, GenerateSpawnPosition(), GenerateQuaternion());
            go.GetComponent<Asteroid>().SetRandomVelocity(100f);
            
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0); ;
    }

    private Quaternion GenerateQuaternion()
    {
        return Quaternion.identity;
    }
}
