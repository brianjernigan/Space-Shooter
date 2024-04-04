using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    private ObjectPool<GameObject> _bulletPool;

    private void Awake()
    {
        //_bulletPool = new ObjectPool<GameObject>(CreateNewBullet(), )
    }

    private void CreateNewBullet()
    {
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(90, 0, 0));
    }

    private void OnBulletRetrieved(GameObject bullet)
    {
        throw new NotImplementedException();
    }

    private void OnBulletReleased(GameObject bullet)
    {
        throw new NotImplementedException();
    }

    private void OnBulletDestroyed(GameObject bullet)
    {
        Destroy(bullet);
    }
}
