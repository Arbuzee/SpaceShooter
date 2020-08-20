using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    [SerializeField] private float aliveForSeconds = 1f;

    void Start()
    {
        StartCoroutine("Kill");
    }

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(aliveForSeconds);
        Destroy(gameObject);
    }
}
