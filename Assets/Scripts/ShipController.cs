using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private const float Speed = 10.0f;
    private const float Tilt = 15.0f;
    private const float BulletSpeed = 100.0f;
    
    private Rigidbody _rb;
    

    [SerializeField] private ParticleSystem _shipParticles;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, 0, verticalInput) * Speed;

        _rb.velocity = new Vector3(movement.x, movement.y, movement.z);

        _rb.rotation = Quaternion.Euler(0, 0, -horizontalInput * Tilt);

        if (Input.GetKey(KeyCode.W) && !_shipParticles.isEmitting)
        {
            _shipParticles.Play();
        }
        else if (!Input.GetKey(KeyCode.W) && _shipParticles.isEmitting)
        {
            _shipParticles.Stop();
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(90, 0, 0));
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(_firePoint.forward * BulletSpeed);
    }
}
