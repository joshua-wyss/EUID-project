using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectiles : MonoBehaviour
{
    [SerializeField] protected float _Speed;
    [SerializeField] Vector3 _targetLocation;

    public virtual void SetTargetLocation(Vector3 t)
    {   
        _targetLocation = t;
        transform.LookAt(_targetLocation);
    }
    public float GetSpeed => _Speed;
}
