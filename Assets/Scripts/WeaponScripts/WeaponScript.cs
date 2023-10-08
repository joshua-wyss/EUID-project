using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    [SerializeField] protected float _reload = 5f;
    [SerializeField] protected Projectiles _projectilePrefab;
    [SerializeField] protected float _lastShotTime =  0;
    [SerializeField] bool needsTransformLock = false;
    protected abstract void FireWeapon(targetLoc tL, float s);  
    public void FireCommand(targetLoc targetPos, float SpeedIncrease)
    {
        if(isReloded())
        {
            FireWeapon(targetPos, SpeedIncrease);
        }
    }
    public virtual float TimeOnTarget()
    {
        return 0f;
    }
    protected bool isReloded()
    {
        return Time.time > _lastShotTime + _reload;
    }
    public float getChargePercent()
    {
        return 100 * (isReloded() ? 1 : (Time.time - _lastShotTime)/_reload);
    }
    protected void SetShotTargetLoc(targetLoc tpos, Projectiles shot, Transform shotOrigin)
    {
        if(_projectilePrefab.isSmartAmo)
        {
            shot.SetTargetLocation(tpos);
        }
        else
        {
            shot.SetTargetLocation(new targetLoc(shotOrigin.transform.position + shotOrigin.forward * 100));
        }
    }
    public float getProjectileSpeed => _projectilePrefab.GetSpeed;
    public bool getLockTReq => needsTransformLock;
}