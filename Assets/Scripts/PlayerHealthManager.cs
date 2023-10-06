using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageAble
{
    [SerializeField] int _Health = 100;
    [SerializeField] int _maxHP = 100;
    [SerializeField] float _ShieldHP = 100;
    [SerializeField] int _maxShieldHP = 100;
    [SerializeField] bool _shieldActive = true;
    [SerializeField] float _shieldRecharge = 5;
    [SerializeField] float _rechargeDelay = 2;
    [SerializeField] float _shieldDisruptionTime = 0;

    public void TakeDamage(int i)
    {
        if(_shieldActive)
        {
            DamageShield(i);
        }
        else
        {
            _Health -= i;
            OnChangeHealth?.Invoke(this, HealthPercent);
            if(_Health <= 0)
            {
                throw new System.NotImplementedException();
            }
        }
    }
    private void Update() {
        if(!ShieldFull && Time.time - _shieldDisruptionTime > _rechargeDelay)
            rechargeShield();
    }
    public event EventHandler<float> OnChangeShield;
    public event EventHandler<bool> OnShieldBreakFull; //send true for shields broken, false for shields reactivated
    public event EventHandler<float> OnChangeHealth;
    private void DamageShield(int i)
    {
        _ShieldHP -= i;
        _shieldDisruptionTime  = Time.time;
        if(_ShieldHP <= 0) 
        {
            _ShieldHP = 0; 
            _shieldActive = false;
            OnShieldBreakFull?.Invoke(this, true);
        }
        OnChangeShield?.Invoke(this, ShieldPercent);
    }
    private void rechargeShield()
    {
        _ShieldHP += _shieldRecharge * Time.deltaTime;
        if(_ShieldHP >= _maxShieldHP)
        {
            _ShieldHP = _maxShieldHP;
            OnShieldBreakFull?.Invoke(this, false);
            _shieldActive = true;
        }
        OnChangeShield?.Invoke(this, ShieldPercent);
    }
    public float HealthPercent => (float)_Health/_maxHP * 100;
    public float ShieldPercent => _ShieldHP / _maxShieldHP * 100;
    private bool ShieldFull => _ShieldHP == _maxShieldHP;
}
