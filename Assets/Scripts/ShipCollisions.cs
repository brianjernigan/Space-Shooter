using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    [SerializeField] private Material _shieldMat;
    [SerializeField] private Material _shipMat;

    [SerializeField] private UIController _ui;
    
    private ShipStats _shipStats;

    private void Awake()
    {
        _shipStats = GetComponent<ShipStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Enemy"))
        {
            _shipStats.TakeDamage(1);
            _ui.UpdateTexts();
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Health"))
        {
            _shipStats.GainHealth(2);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            _shipStats.IsShielded = true;
            gameObject.GetComponent<MeshRenderer>().material = _shieldMat;
            Destroy(other.gameObject);
        }
    }
}
