using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] WeaponScript _weapon;

    private void OnFire()
    {
        _weapon.FireCommand();
    }
    public float GetChargeP()
    {
        return _weapon.getChargePercent();
    }
}
