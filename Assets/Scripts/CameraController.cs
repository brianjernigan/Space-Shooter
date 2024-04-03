using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    
    private readonly Vector3 _offset = new(0, 1f, -1.5f);

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = _playerPosition.transform.position + _offset;
        transform.rotation = Quaternion.Euler(15, 0, 0);
    }
}
