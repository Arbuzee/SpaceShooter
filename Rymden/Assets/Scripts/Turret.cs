using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject target = default;

    [SerializeField] private float fireRate = default;
    private float timeSinceFire = 0f;


    [SerializeField] private GameObject bullet = default;

    // Update is called once per frame
    void Update()
    {
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
        //transform.Rotate(new Vector3(0,0,90));
    }
}
