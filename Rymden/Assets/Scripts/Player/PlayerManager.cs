using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Properties")]
    public float playerStartHealth = 100;

    [Header("Cosmetics")]
    public Gradient playerHealthColor;

    [SerializeField]
    float _playerCurrentHealth;

    GameObject _player;
    MeshRenderer _playerMaterial;
    
    private float health;
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float fireRate = 0.5f;
    private float timeSinceFire = 0f;

    [SerializeField] private GameObject bullet = default;
    [SerializeField] private ParticleSystem deathParticle = default;


    private void Awake()
    {
        _player = transform.GetChild(1).gameObject;
        _playerMaterial = _player.GetComponent<MeshRenderer>();
        _playerCurrentHealth = playerStartHealth;
    }
    
    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float _healthFloat = _playerCurrentHealth / playerStartHealth;
        _playerMaterial.sharedMaterial.SetColor("_EmissionColor", playerHealthColor.Evaluate(_healthFloat) * 5);
        
        // Move to a different script if you want to clean out the shooting mechanics
        timeSinceFire += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (timeSinceFire >= fireRate)
            {
                timeSinceFire = 0f;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }

    private void TakeDamage(float dmg)
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
