using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour , IDamageAble
{
    [SerializeField] int _Health = 50;
    private void Start() {
        _Health = _Health * (int)transform.localScale.x;
    }
    public void TakeDamage(int i)
    {
        _Health -= i;
        if(_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
