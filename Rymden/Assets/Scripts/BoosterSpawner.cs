using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] boosterSpawners = default;
    [SerializeField] private GameObject[] boosters = default;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject go = Instantiate(boosters[0], boosterSpawners[0].transform.position, Quaternion.identity);
            go.GetComponent<Booster>().Fire(new Vector2 (0, -1), 100);

            GameObject go1 = Instantiate(boosters[1], boosterSpawners[1].transform.position, Quaternion.identity);
            go1.GetComponent<Booster>().Fire(new Vector2(0, -1), 100);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = Instantiate(boosters[2], boosterSpawners[2].transform.position, Quaternion.identity);
            go.GetComponent<Booster>().Fire(new Vector2(0, -1), 100);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject go = Instantiate(boosters[3], boosterSpawners[3].transform.position, Quaternion.identity);
            go.GetComponent<Booster>().Fire(new Vector2(0, -1), 100);
        }
    }
}
