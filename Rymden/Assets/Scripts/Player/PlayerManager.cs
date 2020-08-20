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

    private void Awake()
    {
        _player = transform.GetChild(1).gameObject;
        _playerMaterial = _player.GetComponent<MeshRenderer>();
        _playerCurrentHealth = playerStartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float _healthFloat = _playerCurrentHealth / playerStartHealth;
        _playerMaterial.sharedMaterial.SetColor("_EmissionColor", playerHealthColor.Evaluate(_healthFloat) * 5);
    }
}
