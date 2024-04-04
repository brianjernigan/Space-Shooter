using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private BulletManager _bm;

    public bool IsPooled { get; set; } = true;
    private void Start()
    {
        _bm = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletManager>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")) return;

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Hit");
            _bm.BulletPool.Release(gameObject);
        }
    }
}
