using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectiles
{
    [SerializeField] float _fuelTime;  
    [SerializeField] ExplosionScript _explosionScript;
    [SerializeField] GameObject _missileObject;


    private void FixedUpdate() {
        _fuelTime -= Time.deltaTime;
        if(_fuelTime < 0)
        {
            bang();    
        }
        else
        {
            normalStep();
        }
    }
    private void bang()
    {
        _missileObject.SetActive(false);
        _explosionScript.gameObject.SetActive(true);
        this.enabled = false;
    }
    private void normalStep()
    {
        transform.LookAt(_targetLocation.V3);
        transform.position = Vector3.MoveTowards(transform.position, _targetLocation.V3, _Speed * Time.deltaTime);
    }
}
