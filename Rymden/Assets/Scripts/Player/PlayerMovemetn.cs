using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemetn : MonoBehaviour
{
    public float movementSpeed;
    public float boostSpeedMultiplier;
    public float maxVelocity = 20;

    float boosSpeed = 1;

    Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = transform.GetComponent<Rigidbody2D>();
        playerRB.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Debug.Log(playerRB.velocity);
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        playerRB.AddForce(transform.right * ((x * movementSpeed) * boosSpeed));
        playerRB.AddForce(transform.up * ((y * movementSpeed) * boosSpeed));

        if (Input.GetKey(KeyCode.Space))
        {
            boosSpeed = boostSpeedMultiplier;
        }

        if(playerRB.velocity.x > maxVelocity && playerRB.velocity.x < (maxVelocity * -1))
        {
            playerRB.velocity = new Vector2(maxVelocity, playerRB.velocity.y);
        }
    }
}
