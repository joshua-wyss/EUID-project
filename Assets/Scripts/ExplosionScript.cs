using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] Stopwatch _stopwatch = new Stopwatch();
    [SerializeField] float _lifetime = .5f;
    [SerializeField] float _radius = 1f;
    [SerializeField] int _damage = 1;
    [SerializeField] GameObject LaserGo;
    [SerializeField] AudioClip _explosionClip;
    private AudioSource _expSoundSource;

    private void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, _radius);
    }
    private void FixedUpdate() {
        /*if((float)_stopwatch.Elapsed.Milliseconds / 1000 > _lifetime)
        {
            Destroy(LaserGo);
        }*/
        _lifetime -= Time.deltaTime;
        if(_lifetime < 0)
        {
            Destroy(LaserGo);
        }
    }
    private void OnEnable() {
        _stopwatch.Start();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider c in hitColliders)
        {
            IDamageAble damageAble = c.GetComponent<IDamageAble>();
            if(damageAble != null)
            damageAble.TakeDamage(_damage);
        }
        /*
        _expSoundSource = GetComponent<AudioSource>();
        _expSoundSource.clip = _explosionClip;
        _expSoundSource.Play();*/
    }
}   
