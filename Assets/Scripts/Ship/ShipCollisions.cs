//////////////////////////////////////////////
//Assignment/Lab/Project: Space Shooter
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/08/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    private ShipStats _ss;
    private AudioManager _audio;

    private void Awake()
    {
        _ss = GetComponent<ShipStats>();
        _audio = FindObjectOfType<AudioManager>();
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
            _ss.GainHealth(1);
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

    private void OnShieldedHit(GameObject objectToDestroy)
    {
        _ss.IsShielded = false;
        _audio.Block.Play();
        Destroy(objectToDestroy);
    }
}
