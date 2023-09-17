using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFireWeapon : WeaponScript
{
    [SerializeField] List<Transform> _nozzles;
    [SerializeField] bool _linked;
    [SerializeField] int index = 0;
    [SerializeField] int _burstSize;
    [SerializeField] float _cadence;

    protected override void FireWeapon()
    {
        if(_linked)
        {
            foreach (var nozzle in _nozzles)
            {
                StartCoroutine(FireBurst(nozzle));
            }
        }
        else
        {
            StartCoroutine(FireBurst(_nozzles[index]));
            indexIncrement();
        }
        _lastShotTime = Time.time;
    }
    private void indexIncrement()
    {
        index = (index + 1) % _nozzles.Count;
    }
    public IEnumerator FireBurst(Transform nozzle)
    {
        for(int i = 0; i < _burstSize; i++)
        {
            Debug.DrawRay(nozzle.position, nozzle.forward * 100, Color.red, .25f);
            Projectiles shot = Instantiate(_projectilePrefab, nozzle.position, Quaternion.identity);
            shot.SetTargetLocation(nozzle.transform.position + nozzle.forward * 100);
        
            yield return new WaitForSeconds(_cadence);
        }
    }
}
