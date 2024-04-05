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
    
    private GameObject[] _hazards;
    private GameObject[] _pickups;

    private float _hazardSpawnRate = 5f;
    private float _pickupSpawnRate = 2.5f;

    private bool _gameIsOver;
    
    private void Awake()
    {
        _hazards = new[] { _asteroidPrefab, _smallEnemyPrefab, _bigEnemyPrefab };
        _pickups = new[] { _healthPrefab, _shieldPrefab };
    }

    private void Start()
    {
        SpawnSmallEnemy();
        SpawnBigEnemy();
        SpawnAsteroid();
        SpawnHealth();
        SpawnShield();
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
        Instantiate(_bigEnemyPrefab, spawnPos, Quaternion.identity);
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
