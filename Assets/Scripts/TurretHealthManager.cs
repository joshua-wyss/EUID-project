using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealthManager : MonoBehaviour , IDamageAble
{
    [SerializeField] int _Health = 10;

    public void TakeDamage(int i)
    {
        _Health -= i;
        if(_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
