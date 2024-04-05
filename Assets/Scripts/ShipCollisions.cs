using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;
    
    private ShipStats _ss;
    private AudioManager _audio;

    private void Awake()
    {
        _ss = GetComponent<ShipStats>();
        _audio = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _ss.OnShieldedChanged += HandleShipShielded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SmallEnemy"))
        {
            if (_ss.IsShielded)
            {
                OnShieldedHit(other.gameObject);
            }
            else
            {
                _audio.Hit.time = 0.1f;
                _audio.Hit.Play();
                _ss.TakeDamage(1);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("BigEnemy"))
        {
            if (_ss.IsShielded)
            {
                OnShieldedHit(other.gameObject);
            }
            else
            {
                _audio.Hit.time = 0.1f;
                _audio.Hit.Play();
                _ss.TakeDamage(2);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Asteroid"))
        {
            if (_ss.IsShielded)
            {
                OnShieldedHit(other.gameObject);
            }
            else
            {
                _audio.Hit.Play();
                Destroy(other.gameObject);
                _ss.IsStalled = true;
                StartCoroutine(StallShipCorot());
            }
        }
        
        if (other.gameObject.CompareTag("Health"))
        {
            _audio.HealthPickup.Play();
            _ss.GainHealth(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            _audio.ShieldPickup.time = 0.25f;
            _audio.ShieldPickup.Play();
            _ss.IsShielded = true;
            Destroy(other.gameObject);
        }
    }

    private IEnumerator StallShipCorot()
    {
        _audio.Stall.Play();
        yield return new WaitForSeconds(3.0f);
        _ss.IsStalled = false;
        _audio.Stall.Stop();
    }

    private void HandleShipShielded(bool isShielded)
    {
        gameObject.GetComponent<MeshRenderer>().material = isShielded ? _shieldMat : _shipMat;
    }

    private void OnShieldedHit(GameObject objectToDestroy)
    {
        _ss.IsShielded = false;
        _audio.Block.Play();
        Destroy(objectToDestroy);
    }
}
