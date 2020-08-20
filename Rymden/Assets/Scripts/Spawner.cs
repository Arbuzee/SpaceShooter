using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject player = default;

    [Header("Time between enemy spawns")]
    [SerializeField] private float spawnRate = default;
    [SerializeField] private GameObject[] enemies = default;

    [SerializeField] private float minSpawnRadius = default;
    [SerializeField] private float maxSpawnRadius = default;

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
                GenerateSpawnPosition() + player.transform.position, 
                GenerateQuaternion());

            if (spawnIndex == 0)
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
        return new Vector3(
            Random.Range(-Random.Range(-maxSpawnRadius, minSpawnRadius), Random.Range(-minSpawnRadius, maxSpawnRadius)), 
            Random.Range(-Random.Range(-maxSpawnRadius, minSpawnRadius), Random.Range(-minSpawnRadius, maxSpawnRadius)), 
            0); ;
    }

    private Quaternion GenerateQuaternion()
    {
        return Quaternion.identity;
    }
}
