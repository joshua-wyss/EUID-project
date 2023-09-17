using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{//old
    [SerializeField] Transform ExitPoint;
    [SerializeField] int Burst = 2;
    [SerializeField] float Cadence = .5f;
    [SerializeField] float reload = 5f;
    [SerializeField] bool fireing = false;
    [SerializeField] readonly Stopwatch _stopwatch = new Stopwatch();
    private void OnDrawGizmos() {
       Gizmos.color = Color.red;
       Gizmos.DrawSphere(ExitPoint.position ,.1f);
    }
    private void OnFire()
    {
        Trigger();
    }
    private void Trigger()
    {
        if(!fireing) 
        {
            //Debug.Log("fire");
            StartCoroutine(FireBurst());
        }
        //else Debug.Log("na");
    }
    private void FireWeapon()
    {
        UnityEngine.Debug.DrawRay(ExitPoint.position, ExitPoint.forward * 100, Color.red);
        //Debug.Log("fired weapon");
    }
    public IEnumerator FireBurst()
    {
        fireing = true;
        for(int i = 0; i < Burst; i++)
        {
            FireWeapon();
            yield return new WaitForSeconds(Cadence);
        }
        _stopwatch.Start();
        yield return new WaitForSeconds(reload);
        _stopwatch.Stop();
        _stopwatch.Reset();
        fireing = false;
    }
    public float GetChargeP()
    {
        //return fireing ? (timer/reload)*100 : 100;
        return fireing ? (float)(_stopwatch.Elapsed.TotalSeconds/reload * 100) : 100;
    }
}
