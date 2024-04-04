using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("asteroid");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            Debug.Log("Health");
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Shield");
        }
    }
}
