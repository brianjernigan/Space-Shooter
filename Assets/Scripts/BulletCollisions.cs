using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")) return;

        Destroy(gameObject);
    }
}
