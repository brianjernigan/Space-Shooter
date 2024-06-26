//////////////////////////////////////////////
//Assignment/Lab/Project: Space Shooter
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 04/08/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _asteroidDestroy;
    public AudioSource AsteroidDestroy => _asteroidDestroy;
    
    [SerializeField] private AudioSource _block;
    public AudioSource Block => _block;
    
    [SerializeField] private AudioSource _fire;
    public AudioSource Fire => _fire;
    
    [SerializeField] private AudioSource _healthPickup;
    public AudioSource HealthPickup => _healthPickup;

    [SerializeField] private AudioSource _hit;
    public AudioSource Hit => _hit;

    [SerializeField] private AudioSource _poof;
    public AudioSource Poof => _poof;
    
    [SerializeField] private AudioSource _shieldPickup;
    public AudioSource ShieldPickup => _shieldPickup;
    
    [SerializeField] private AudioSource _shipDestroy;
    public AudioSource ShipDestroy => _shipDestroy;

    [SerializeField] private AudioSource _stall;
    public AudioSource Stall => _stall;
    
    [SerializeField] private AudioSource _thrusters;
    public AudioSource Thrusters => _thrusters;
}
