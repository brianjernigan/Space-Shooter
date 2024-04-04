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
    public ObjectPool<GameObject> BulletPool => _bulletPool;

    private void Awake()
    {
        _bulletPool =
            new ObjectPool<GameObject>(CreateNewBullet, OnBulletRetrieved, 
                OnBulletReleased, OnBulletDestroyed, 
                false, 5, 10);
    }

    private GameObject CreateNewBullet()
    {
        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(90, 0, 0));
        bullet.SetActive(false);
        return bullet;
    }

    private void OnBulletRetrieved(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = _firePoint.position;
        StartCoroutine(BulletLifecycleCorot(bullet));
    }

    private void OnBulletReleased(GameObject bullet)
    {
        bullet.SetActive(false);
        StopCoroutine(BulletLifecycleCorot(bullet));
    }

    private void OnBulletDestroyed(GameObject bullet)
    {
        Destroy(bullet);
    }

    private IEnumerator BulletLifecycleCorot(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        _bulletPool.Release(bullet);
    }
}
