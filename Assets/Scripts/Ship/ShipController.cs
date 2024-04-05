//////////////////////////////////////////////
//Assignment/Lab/Project: Space Shooter
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/08/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private const float Speed = 10.0f;
    private const float Tilt = 15.0f;
    private const float BulletSpeed = 50.0f;
    
    private Rigidbody _rb;
    private BulletManager _bm;
    private ShipStats _ss;
    private AudioManager _audio;

    [SerializeField] private ParticleSystem _shipParticles;
    [SerializeField] private Transform _firePoint;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bm = GetComponent<BulletManager>();
        _ss = GetComponent<ShipStats>();
        _audio = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_ss.IsStalled)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Move();   
        HandleParticles();
        HandleThrusterAudio();
    }

    private void Move()
    {
        if (_ss.IsStalled || _ss.IsDead) return;
        
        var horizontalInput = Input.GetAxis("Horizontal");

        var movement = new Vector3(horizontalInput, 0, 0) * Speed;

        _rb.velocity = new Vector3(movement.x, movement.y, movement.z);

        _rb.rotation = Quaternion.Euler(0, 0, -horizontalInput * Tilt);
    }

    private void HandleParticles()
    {
        if (_ss.IsDead)
        {
            _shipParticles.Stop();
            return;
        }
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !_shipParticles.isEmitting && !_ss.IsStalled)
        {
            _shipParticles.Play();
        }
        else if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || _ss.IsStalled) && _shipParticles.isEmitting)
        {
            _shipParticles.Stop();
        }
    }

    private void HandleThrusterAudio()
    {
        if (_ss.IsDead)
        {
            _audio.Thrusters.Stop();
        }
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !_ss.IsStalled)
        {
            if (!_audio.Thrusters.isPlaying)
            {
                _audio.Thrusters.Play();
            }
        }
        else
        {
            _audio.Thrusters.Stop();
        }
    }

    private void Shoot()
    {
        _audio.Fire.Play();
        var bullet = _bm.BulletPool.Get();
        var bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(_firePoint.forward * BulletSpeed);
    }
}
