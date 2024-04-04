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
    
    [SerializeField] private AudioSource _shieldPickup;
    public AudioSource ShieldPickup => _shieldPickup;
    
    [SerializeField] private AudioSource _shipDestroy;
    public AudioSource ShipDestroy => _shipDestroy;
    
    [SerializeField] private AudioSource _thrusters;
    public AudioSource Thrusters => _thrusters;
}
