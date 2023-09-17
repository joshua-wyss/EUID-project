using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    [SerializeField] protected float _reload = 5f;
    [SerializeField] protected Projectiles _projectilePrefab;
    [SerializeField] protected float _lastShotTime =  0;
    protected abstract void FireWeapon();    
    public void FireCommand()
    {
        if(isReloded())
        {
            FireWeapon();
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
    public float getProjectileSpeed => _projectilePrefab.GetSpeed;
}