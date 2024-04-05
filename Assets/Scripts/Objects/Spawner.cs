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

    private const float HazardSpawnRate = 2f;
    private const float PickupSpawnRate = 8f;

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
        while (!_ss.IsDead)
        {
            SpawnRandomHazard();
            yield return new WaitForSeconds(HazardSpawnRate);
        }
    }

    private IEnumerator SpawnPickupCorot()
    {
        while (!_ss.IsDead)
        {
            SpawnRandomPickup();
            yield return new WaitForSeconds(PickupSpawnRate);
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

    private void SpawnObject(GameObject prefab, float yPos, Quaternion rotation)
    {
        var spawnPos = GenerateSpawnPoint();
        spawnPos.y = yPos;
        Instantiate(prefab, spawnPos, rotation);
    }

    private void SpawnSmallEnemy()
    {
        SpawnObject(_smallEnemyPrefab, 0.3f, Quaternion.Euler(0, 180, 0));
    }

    private void SpawnBigEnemy()
    {
        SpawnObject(_bigEnemyPrefab, 1f, Quaternion.Euler(0, 180, 0));
    }

    private void SpawnAsteroid()
    {
        SpawnObject(_asteroidPrefab, 1f, Quaternion.identity);
    }

    private void SpawnHealth()
    {
        SpawnObject(_healthPrefab, 0.6f, Quaternion.identity);
    }

    private void SpawnShield()
    {
        SpawnObject(_shieldPrefab, 0.75f, Quaternion.identity);
    }
}
