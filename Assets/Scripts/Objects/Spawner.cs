using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Hazards")]
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private GameObject _smallEnemyPrefab;
    [SerializeField] private GameObject _bigEnemyPrefab;

    [Header("Pickups")] 
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private GameObject _shieldPrefab;

    [Header("Spawn Points")] 
    [SerializeField] private Transform _minSpawnPoint;
    [SerializeField] private Transform _maxSpawnPoint;

    private float _hazardSpawnRate = 2f;
    private float _pickupSpawnRate = 5f;

    private bool _gameIsOver;

    private ShipStats _ss;

    private void Awake()
    {
        _ss = FindObjectOfType<ShipStats>();
    }

    private void Start()
    {
        StartCoroutine(SpawnHazardCorot());
        StartCoroutine(SpawnPickupCorot());
    }

    private IEnumerator SpawnHazardCorot()
    {
        while (!_gameIsOver)
        {
            SpawnRandomHazard();
            yield return new WaitForSeconds(_hazardSpawnRate);
        }
    }

    private IEnumerator SpawnPickupCorot()
    {
        while (!_gameIsOver)
        {
            SpawnRandomPickup();
            yield return new WaitForSeconds(_pickupSpawnRate);
        }
    }

    private void SpawnRandomHazard()
    {
        var randomHazard = Random.Range(1, 4);
        switch (randomHazard)
        {
            case 1:
                SpawnSmallEnemy();
                break;
            case 2:
                SpawnBigEnemy();
                break;
            case 3:
                SpawnAsteroid();
                break;
        }
    }

    private void SpawnRandomPickup()
    {
        if (_ss.IsShielded && _ss.Health == ShipStats.MaxHealth) return;
        
        if (_ss.IsShielded)
        {
            SpawnHealth();
        } else if (_ss.Health == ShipStats.MaxHealth) 
        {
            SpawnShield();
        }
        else
        {
            var randomPickup = Random.Range(1, 3);
            switch (randomPickup)
            {
                case 1:
                    SpawnHealth();
                    break;
                case 2:
                    SpawnShield();
                    break;
            }
        }
    }

    private Vector3 GenerateSpawnPoint()
    {
        var minPos = _minSpawnPoint.position;
        var maxPos = _maxSpawnPoint.position;
        var spawnPoint = new Vector3(Random.Range(minPos.x, maxPos.x), 0,
            Random.Range(minPos.z, maxPos.z));
        return spawnPoint;
    }

    private void SpawnSmallEnemy()
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = 0.3f;
        Instantiate(_smallEnemyPrefab, spawnPos, Quaternion.Euler(0, 180, 0));
    }

    private void SpawnBigEnemy()
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = 1f;
        Instantiate(_bigEnemyPrefab, spawnPos, Quaternion.Euler(0, 180, 0));
    }

    private void SpawnAsteroid()
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = 1f;
        Instantiate(_asteroidPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnHealth()
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = 0.6f;
        Instantiate(_healthPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnShield()
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = 0.75f;
        Instantiate(_shieldPrefab, spawnPos, Quaternion.identity);
    }

    private void StopSpawning()
    {
        _gameIsOver = true;
    }
}
