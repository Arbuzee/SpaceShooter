using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    public float movementSpeed;
    [Tooltip("Player's top speed")]
    public float maxVelocity = 20.0f;
    [Tooltip("Big value = Fast break | Low value = Slow break\nRecommended value: 0.2")]
    public float slowDown = 0.2f;
    public float sprintMultiplier = 2;
    [HideInInspector]
    public bool playerIsBoosting;

    //Mouse variables
    GameObject _playerLookpoint;
    GameObject _playerModel;
    [SerializeField] Camera _playerCamera;

    Rigidbody2D _playerRB;

    private Vector2 movement;

    private void Awake()
    {
        _playerRB = transform.GetComponent<Rigidbody2D>();
        _playerRB.gravityScale = 0;                         //Turn of gravity (IN SPACE? WHAT IRON????)
        _playerLookpoint = transform.GetChild(0).gameObject;
        _playerModel = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        //Debug.Log(_playerRB.velocity.sqrMagnitude);
        PlayerLook();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ManageInput()
    {
        movement.x = Input.GetAxis("Horizontal");      //Declares if the player goes right/left
        movement.y = Input.GetAxis("Vertical");        //Declares if the player goes up/down
    }

    void Move()
    {
        /*
        _playerRB.AddForce(transform.right * ((movement.x * movementSpeed) * 2));       //Applies force right/left
        _playerRB.AddForce(transform.up * ((movement.y * movementSpeed) * 2));          //Applies force up/down
        */
        #region Player Speed

        #region Max Speed Manager
        //Speed capper

        // velocity.x > 0 höger , < 0 vänster
        // velocity.y > 0 upp, < 0 ner


        if (_playerRB.velocity.sqrMagnitude > maxVelocity) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-_playerRB.velocity.normalized * movementSpeed * 5);
        } else
        {
            _playerRB.AddForce(movement * movementSpeed);       //Applies force right/left
        }

        /*
        if (_playerRB.velocity.x > maxVelocity && movement.x != -1 || _playerRB.velocity.x < (maxVelocity * -1) && movement.x != 1) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.right * ((movement.x * movementSpeed) * 2));
        }

        if (_playerRB.velocity.y > maxVelocity && movement.y != -1 || _playerRB.velocity.y < (maxVelocity * -1) && movement.y != 1) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.up * ((movement.y * movementSpeed) * 2));
        }*/

        #endregion

        #region Speed Breaker
        //Velocity Break
        /*
        if (Input.GetAxis("Horizontal") == 0)       //If no input slow player down
        {
            _playerRB.AddForce(-transform.right * (_playerRB.velocity.x * slowDown));
        }

        if (Input.GetAxis("Vertical") == 0)         //If no input slow player down
        {
            _playerRB.AddForce(-transform.up * (_playerRB.velocity.y * slowDown));
        }
        */
        #endregion

        #endregion
    }

    void PlayerLook()
    {

        Vector3 worldPos;
        Vector3 mousePos = Input.mousePosition;                     //Gets mouse pos
        worldPos = _playerCamera.ScreenToWorldPoint(mousePos);      //Converts mouse pos to world pos

        _playerLookpoint.transform.position = new Vector3(worldPos.x, worldPos.y, 0f);

        /*
        _playerModel.transform.LookAt(_playerLookpoint.transform.position);     //Turns player to mouse
        _playerModel.transform.rotation = Quaternion.Euler(new Vector3(_playerModel.transform.rotation.x, 180, 0));
        */

        transform.forward = -(new Vector3(worldPos.x, worldPos.y, 32) - transform.position);
    }
}
