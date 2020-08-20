using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        transform.localScale = (transform.localScale * .9f);
    }

    public void Fire(Vector2 dir, float force)
    {
        rb2d.AddForce(dir * force);
    }
}
