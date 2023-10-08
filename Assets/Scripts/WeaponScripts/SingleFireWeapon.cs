using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireWeapon : WeaponScript
{
    [SerializeField] Transform _nozzle;

    protected override void FireWeapon(targetLoc targetPos, float speedIncrease)
    {
        Debug.DrawRay(_nozzle.position, _nozzle.forward * 100, Color.red, .5f);
        Projectiles shot = Instantiate(_projectilePrefab, _nozzle.position, Quaternion.identity);
        /*if(_projectilePrefab.isSmartAmo)
        {
            shot.SetTargetLocation(targetPos);
        }
        else
        {
            shot.SetTargetLocation(_nozzle.transform.position + _nozzle.forward * 100);
        }*/
        SetShotTargetLoc(targetPos, shot, _nozzle);
        shot.SpeedIncrease(speedIncrease);
        _lastShotTime = Time.time;  
    }
}
