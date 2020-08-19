using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemetn : MonoBehaviour
{
    [Header("Player Properties")]
    public float movementSpeed;
    [Tooltip("Player's top speed")]
    public float maxVelocity = 20.0f;
    [Tooltip("Big value = Fast break | Low value = Slow break\nRecommended value: 0.2")]
    public float slowDown = 0.2f;

    float _boosSpeed = 2;
    Rigidbody2D _playerRB;

    private void Awake()
    {
        _playerRB = transform.GetComponent<Rigidbody2D>();
        _playerRB.gravityScale = 0;                         //Turn of gravity (IN SPACE? WHAT IRON????)
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");      //Declares if the player goes right/left
        float y = Input.GetAxis("Vertical");        //Declares if the player goes up/down

        _playerRB.AddForce(transform.right * ((x * movementSpeed) * _boosSpeed));       //Applies force right/left
        _playerRB.AddForce(transform.up * ((y * movementSpeed) * _boosSpeed));          //Applies force up/down

        #region Player Speed

        #region Max Speed Manager
        //Speed capper
        if (_playerRB.velocity.x > maxVelocity || _playerRB.velocity.x < (maxVelocity * -1)) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.right * ((x * movementSpeed) * _boosSpeed));
        }

        if (_playerRB.velocity.y > maxVelocity || _playerRB.velocity.y < (maxVelocity * -1)) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.up * ((y * movementSpeed) * _boosSpeed));
        }
        #endregion

        #region Speed Breaker
        //Velocity Break
        if (Input.GetAxis("Horizontal") == 0)       //If no input slow player down
        {
            _playerRB.AddForce(-transform.right * (_playerRB.velocity.x * slowDown));
        }

        if (Input.GetAxis("Vertical") == 0)         //If no input slow player down
        {
            _playerRB.AddForce(-transform.up * (_playerRB.velocity.y * slowDown));
        }

        #endregion

        #endregion
    }
}
