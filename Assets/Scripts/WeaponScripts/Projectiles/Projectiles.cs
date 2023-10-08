using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectiles : MonoBehaviour
{
    [SerializeField] protected float _Speed;
    [SerializeField] protected targetLoc _targetLocation;

    public virtual void SetTargetLocation(targetLoc t)
    {   
        //_targetLocation = new targetLoc(t);
        _targetLocation = t;
        transform.LookAt(_targetLocation.V3);
    }
    public virtual void SpeedIncrease(float increase)
    {
        _Speed += increase;
    }
    public float GetSpeed => _Speed;
    public virtual bool isSmartAmo => false;
}
[System.Serializable]
public class targetLoc{
    [SerializeField] Vector3 _targetV3;
    [SerializeField] Transform _targetT;
    [SerializeField] bool _isV3 = true;

    public targetLoc(Vector3 v3)
    {
        _targetV3 = v3;
    }
    public targetLoc(Transform transform)
    {
        _isV3 = false;
        _targetT = transform;
        _targetV3 = transform.position;
    }
    public Vector3 GetTransformPos()
    {
        if(_targetT != null)
            return _targetT.position;
        return _targetV3;
    }
    public Vector3 V3 => _isV3 ? _targetV3 : GetTransformPos();
}