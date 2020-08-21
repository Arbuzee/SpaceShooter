using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject _player;
    MeshRenderer _playerMaterial;
    GameObject _playerModel;
    GameObject _playerLookPoint;

    [SerializeField] private Transform firePointLeft = default;
    [SerializeField] private Transform firePointRight = default;
    private bool hasFiredFromLeft = false;

    private float health;
    [Header("Player Properties")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceFire = 0f;

    [Header("Cosmetics")]
    public Gradient playerHealthColor;

    [Header("Prefabs")]
    [SerializeField] private GameObject bullet = default;
    [SerializeField] private ParticleSystem deathParticle = default;


    private void Awake()
    {
        _player = transform.GetChild(1).gameObject;
        _playerMaterial = _player.GetComponent<MeshRenderer>();

        _playerModel = transform.GetChild(1).gameObject;
        _playerLookPoint = transform.GetChild(0).gameObject;
    }
    
    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float _healthFloat = health / maxHealth;
        _playerMaterial.sharedMaterial.SetColor("_EmissionColor", playerHealthColor.Evaluate(_healthFloat) * 5);
        
        // Move to a different script if you want to clean out the shooting mechanics
        timeSinceFire += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (timeSinceFire >= fireRate)
            {
                timeSinceFire = 0f;

                firePointLeft.right = _playerLookPoint.transform.position - transform.position;
                firePointRight.right = _playerLookPoint.transform.position - transform.position;

                if (hasFiredFromLeft)
                {
                    Instantiate(
                        bullet,
                        firePointRight.transform.position,
                        Quaternion.Euler(firePointRight.transform.position));
                }
                else
                {
                    Instantiate(
                        bullet,
                        firePointLeft.transform.position,
                        Quaternion.Euler(firePointLeft.transform.position));
                }
                


                Debug.DrawLine(transform.position, transform.position + (_playerLookPoint.transform.position - transform.position).normalized * 10, Color.red, Mathf.Infinity);
                hasFiredFromLeft = !hasFiredFromLeft;
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

}
