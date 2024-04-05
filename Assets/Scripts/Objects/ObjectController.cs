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

public class ObjectController : MonoBehaviour
{
    private const float BackWallZPos = -61f;
    
    private ShipStats _ss;
    private AudioManager _audio;

    private float _speed;
    
    private void Awake()
    {
        _ss = FindObjectOfType<ShipStats>();
        _audio = FindObjectOfType<AudioManager>();
        
        DetermineSpeed();
    }

    private void DetermineSpeed()
    {
        switch (gameObject.tag)
        {
            case "BigEnemy":
                _speed = 1.5f * _ss.Modifier;
                break;
            case "SmallEnemy":
                _speed = 3f * _ss.Modifier;
                break;
            case "Asteroid":
                _speed = 2.5f * _ss.Modifier;
                break;
            case "Health":
            case "Shield":
                _speed = 2f * _ss.Modifier;
                break;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var position = transform.position;
        position = Vector3.MoveTowards(position,
            new Vector3(position.x, position.y, BackWallZPos), Time.deltaTime * _speed);
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("BackWall")) return;

        if (!ObjectIsHazardous())
        {
            _audio.Poof.time = 0.5f;
            _audio.Poof.Play();
        }
        else
        {
            if (_ss.IsShielded)
            {
                _ss.IsShielded = false;
                _audio.Block.Play();
            }
            else
            {
                _audio.Hit.time = 0.1f;
                _audio.Hit.Play();
                _ss.TakeDamage(1);
            }
        }

        Destroy(gameObject);
    }

    private bool ObjectIsHazardous()
    {
        return !gameObject.CompareTag("Health") && !gameObject.CompareTag("Shield");
    }
}
