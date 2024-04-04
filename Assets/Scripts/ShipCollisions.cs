using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;
    
    private ShipStats _ss;
    private ShipController _sc;

    private void Awake()
    {
        _ss = GetComponent<ShipStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SmallEnemy"))
        {
            _ss.TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("BigEnemy"))
        {
            _ss.TakeDamage(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Asteroid"))
        {
            
        }
        
        if (other.gameObject.CompareTag("Health"))
        {
            _ss.GainHealth(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            _ss.IsShielded = true;
            gameObject.GetComponent<MeshRenderer>().material = _shieldMat;
            Destroy(other.gameObject);
        }
    }
}
