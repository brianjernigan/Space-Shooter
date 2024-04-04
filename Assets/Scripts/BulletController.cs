using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private BulletManager _bm;
    private void Start()
    {
        _bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletManager>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            _bm.BulletPool.Release(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground")) return;

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Hit");
            _bm.BulletPool.Release(gameObject);
        }
    }
}
