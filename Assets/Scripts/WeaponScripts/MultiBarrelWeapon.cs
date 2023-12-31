using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiBarrelWeapon : WeaponScript
{
    [SerializeField] List<Transform> _nozzles;
    [SerializeField] bool _linked;
    [SerializeField] int index = 0;

    protected override void FireWeapon(targetLoc targetPos, float speedIncrease)
    {
        if(_linked)
        {
            foreach (var nozzle in _nozzles)
            {
                Debug.DrawRay(nozzle.position, nozzle.forward * 100, Color.red, .5f);
                FireShot(targetPos, nozzle, speedIncrease);
            }
        }
        else
        {
            Debug.DrawRay(_nozzles[index].position, _nozzles[index].forward * 100, Color.red, .5f);
            FireShot(targetPos, _nozzles[index], speedIncrease);
            indexIncrement();
        }
        _lastShotTime = Time.time;
    }
    private void indexIncrement()
    {
        index = (index + 1) % _nozzles.Count;
    }
    private void FireShot(targetLoc targetPos, Transform nozzle, float speedIncrease)
    {
        PlaySound();
        Projectiles shot = Instantiate(_projectilePrefab, nozzle.position, Quaternion.identity);
        //shot.SetTargetLocation(nozzle.transform.position + nozzle.forward * 100);
        SetShotTargetLoc(targetPos, shot, nozzle);
        shot.SpeedIncrease(speedIncrease);
    }
}
