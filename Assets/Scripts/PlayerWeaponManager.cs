using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] WeaponScript _weapon;
    [SerializeField] bool _fireIsPressed = false;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _lockedTransform;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnFire(InputValue inputValue)
    {
        //Debug.Log(_weapon.getLockTReq + " " + _lockedTransform != null);
        if(_weapon.getLockTReq && _lockedTransform != null)
        {
            _weapon.FireCommand(new targetLoc(_lockedTransform), _rigidbody.velocity.magnitude);
            Debug.Log("fired with lock");
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
    public event EventHandler<float> OnChangeCharge;
}
