using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireWeapon : WeaponScript
{
    [SerializeField] Transform _nozzle;

    protected override void FireWeapon()
    {
        Debug.DrawRay(_nozzle.position, _nozzle.forward * 100, Color.red, .5f);
        Projectiles shot = Instantiate(_projectilePrefab, _nozzle.position, Quaternion.identity);
        shot.SetTargetLocation(_nozzle.transform.position + _nozzle.forward * 100);
        _lastShotTime = Time.time;  
    }
}
