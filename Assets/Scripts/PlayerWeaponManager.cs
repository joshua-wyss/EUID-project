using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] WeaponScript _weapon;
    [SerializeField] List<WeaponScript> _avaialbleWeapons = new List<WeaponScript>(); 
    [SerializeField] int _weaponIndex = 0;
    [SerializeField] bool _fireIsPressed = false;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _lockedTransform;
    [SerializeField] float _lookOnRadius = 100;
    [SerializeField] float _lookOnAngle = 80;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _weapon = _avaialbleWeapons[0];
    }
    private void OnFire(InputValue inputValue)
    {
        //Debug.Log(_weapon.getLockTReq + " " + _lockedTransform != null);
        if(_weapon.getLockTReq && _lockedTransform != null)
        {
            _weapon.FireCommand(new targetLoc(_lockedTransform), _rigidbody.velocity.magnitude);
            //Debug.Log("fired with lock");
        }
        else
        {            
            _weapon.FireCommand(new targetLoc(transform.forward), _rigidbody.velocity.magnitude);
            _fireIsPressed = inputValue.isPressed;
        }
    }
    public float GetChargeP()
    {
        return _weapon.getChargePercent();
    }
    private void FixedUpdate() {
        OnChangeCharge?.Invoke(this, GetChargeP());
    }
    public void OnSeekLock()
    {
        SeekingLock();
    }
    public void OnSwapWeapon()
    {
        _weaponIndex++;
        if(_weaponIndex >= _avaialbleWeapons.Count)
            _weaponIndex = 0;
        _weapon = _avaialbleWeapons[_weaponIndex];
    }
    private void SeekingLock()
    {
        Debug.Log("seeking lock");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _lookOnRadius);
        
        List<GameObject> targetList = new List<GameObject>();
        foreach (Collider c in hitColliders)
        {
            Vector3 dir = c.gameObject.transform.position - transform.position;
            IDamageAble damageAble = c.GetComponent<IDamageAble>();
            if(damageAble != null && Vector3.Angle(dir, transform.forward) <= _lookOnRadius && c.gameObject.tag == "Hostile")
            {
                targetList.Add(c.gameObject);
            }
        }
        if(targetList.Count > 0)
        {
            GameObject closest = targetList[0];
            float smallestAngle = Vector3.Angle(closest.gameObject.transform.position - transform.position, transform.forward);
            foreach (var item in targetList)
            {
                //Debug.DrawRay(item.transform.position, item.transform.up * 20, Color.magenta, 2f);
                float x = Vector3.Angle(item.gameObject.transform.position - transform.position, transform.forward);
                if(x < smallestAngle)
                {
                    smallestAngle = x;
                    closest = item;
                }
            }
            _lockedTransform = closest.transform;
        }
    }
    public event EventHandler<float> OnChangeCharge;
}
