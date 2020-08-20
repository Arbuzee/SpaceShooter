using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject player = default;

    [Header("Time between enemy spawns")]
    [SerializeField] private float spawnRate = default;
    [SerializeField] private GameObject[] enemies = default;

    [SerializeField] private float spawnRadius = default;

    private float timeSinceSpawn = 0f;

    private void Update()
    {
        spawnRate -= Time.deltaTime * 0.001f; // Make it increase with score maybe?

        Debug.Log(spawnRate);

        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnRate)
        {
            timeSinceSpawn = 0;

            int spawnIndex = SelectEnemyToSpawn();
            GameObject go = Instantiate(
                enemies[spawnIndex], 
                GenerateSpawnPosition(), 
                GenerateQuaternion());

            if (spawnIndex == 1)
            {
                go.GetComponent<Turret>().SetPlayer(player);
            }
        }
    }

    private int SelectEnemyToSpawn()
    {
        return Random.Range(0, enemies.Length);
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
