using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public float distance = 5f;

    void Update()
    {
        Vector3 direction = _target.position - transform.position;

        Vector3 newPosition = _target.position - direction.normalized * distance;

        transform.position = newPosition;
    }
}