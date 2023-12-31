using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectiles
{
    [SerializeField] float _fuelTime;  
    [SerializeField] ExplosionScript _explosionScript;
    [SerializeField] GameObject _missileObject;
    [SerializeField] float _inactivePeriod = .5f;


    private void FixedUpdate() {
        _fuelTime -= Time.deltaTime;
        _inactivePeriod -= Time.deltaTime;
        if(_fuelTime < 0)
        {
            bang();    
        }
        /*
        else if(EndPoint != Vector3.positiveInfinity)
        {
            bangAtEndPoint();
        }*/
        else
        {
            normalStep();
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(_inactivePeriod < 0)
            bang();
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
    public override bool isSmartAmo => true;
}
