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
        Move();
        Debug.Log(_playerRB.velocity);
        PlayerLook();
    }

    void Move()
    {
        float _x = Input.GetAxis("Horizontal");      //Declares if the player goes right/left
        float _y = Input.GetAxis("Vertical");        //Declares if the player goes up/down

        _playerRB.AddForce(transform.right * ((_x * movementSpeed) * 2));       //Applies force right/left
        _playerRB.AddForce(transform.up * ((_y * movementSpeed) * 2));          //Applies force up/down

        #region Player Speed

        #region Max Speed Manager
        //Speed capper
        if (_playerRB.velocity.x > maxVelocity && _x != -1 || _playerRB.velocity.x < (maxVelocity * -1) && _x != 1) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.right * ((_x * movementSpeed) * 2));
        }

        if (_playerRB.velocity.y > maxVelocity && _y != -1 || _playerRB.velocity.y < (maxVelocity * -1) && _y != 1) //If the player moves too fast to the right/left
        {
            _playerRB.AddForce(-transform.up * ((_y * movementSpeed) * 2));
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

    void PlayerLook()
    {
        Vector3 worldPos;
        Vector3 mousePos = Input.mousePosition;                     //Gets mouse pos
        worldPos = _playerCamera.ScreenToWorldPoint(mousePos);      //Converts mouse pos to world pos

        _playerLookpoint.transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);

        _playerModel.transform.LookAt(_playerLookpoint.transform.position);     //Turns player to mouse
    }
}
