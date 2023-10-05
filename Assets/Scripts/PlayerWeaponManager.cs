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

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnFire(InputValue inputValue)
    {
        _weapon.FireCommand(transform.forward, _rigidbody.velocity.magnitude);
        _fireIsPressed = inputValue.isPressed;
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
