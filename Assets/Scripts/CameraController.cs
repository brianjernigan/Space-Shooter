using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    
    private readonly Vector3 _offset = new(0, 3.25f, -1f);

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = _playerPosition.transform.position + _offset;
        transform.rotation = Quaternion.Euler(45f, 0, 0);
    }
}
