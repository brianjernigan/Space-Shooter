using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    private const float Tumble = 0.5f;
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Tumble;
    }
}