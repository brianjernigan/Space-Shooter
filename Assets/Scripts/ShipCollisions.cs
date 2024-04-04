using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    private ShipStats _shipStats;

    private void Awake()
    {
        _shipStats = GetComponent<ShipStats>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            _shipStats.TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            _shipStats.GainHealth(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            _shipStats.IsShielded = true;
            Destroy(other.gameObject);
        }
    }
}
