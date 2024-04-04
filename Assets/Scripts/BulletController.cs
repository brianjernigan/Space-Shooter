using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private BulletManager _bm;
    private ShipStats _ss;
    
    private void Start()
    {
        _bm = FindObjectOfType<BulletManager>();
        _ss = FindObjectOfType<ShipStats>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground")) return;
        
        if (other.gameObject.CompareTag("Environment"))
        {
            _bm.BulletPool.Release(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BigEnemy"))
        {
            _ss.IncreaseScore(3);
            Destroy(other.gameObject);
            _bm.BulletPool.Release(gameObject);
        }

        if (other.gameObject.CompareTag("SmallEnemy"))
        {
            _ss.IncreaseScore(2);
            Destroy(other.gameObject);
            _bm.BulletPool.Release(gameObject);
        }
        
        if (other.gameObject.CompareTag("Asteroid"))
        {
            _ss.IncreaseScore(1);
            Destroy(other.gameObject);
            _bm.BulletPool.Release(gameObject);
        }
    }
}
