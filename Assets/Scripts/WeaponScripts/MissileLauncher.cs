using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : WeaponScript
{
    [SerializeField] Transform _nozzle;
    protected override void FireWeapon(targetLoc targetLoc, float s)
    {
        Projectiles missile = Instantiate(_projectilePrefab, _nozzle.position, Quaternion.identity);
        missile.SetTargetLocation(targetLoc);
        missile.SpeedIncrease(s);
    }
}
