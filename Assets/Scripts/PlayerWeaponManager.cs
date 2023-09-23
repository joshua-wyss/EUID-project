using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] WeaponScript _weapon;
    [SerializeField] bool _fireIsPressed = false;

    private void OnFire(InputValue inputValue)
    {
        _weapon.FireCommand(transform.forward);
        _fireIsPressed = inputValue.isPressed;
    }
    public float GetChargeP()
    {
        return _weapon.getChargePercent();
    }
}
