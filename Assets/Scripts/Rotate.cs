using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 rotateDir;

    private void Update()
    {
        transform.Rotate(rotateDir * _speed * Time.deltaTime, Space.Self);
    }
}
