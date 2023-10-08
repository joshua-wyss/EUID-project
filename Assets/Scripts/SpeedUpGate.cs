using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpGate : MonoBehaviour
{
    [SerializeField] int ForceApplied;
    [SerializeField] float duration;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            EnterSpeedUp();
        }
    }
    public event EventHandler<SpeedUpData> OnEnterdSpeedUp;
    protected virtual void EnterSpeedUp()
    {
        //Debug.Log("Sending SpeedUpEvent");
        OnEnterdSpeedUp?.Invoke(this, new SpeedUpData(ForceApplied, duration));
    }
}
public class SpeedUpData : EventArgs
{
    public int Force {get; set;}
    public float Duration {get; set;}
    public SpeedUpData(int f, float d)    {
        this.Force = f; this.Duration = d;
    }
}
