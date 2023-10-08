using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMCG : MonoBehaviour
{

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
        {
            EnteredPickUp();
        }
    }
    public event EventHandler<PickUpData> OnEnterPickUp;

    private void EnteredPickUp()
    {
        OnEnterPickUp?.Invoke(this, new PickUpData(this));
    }

    public void PickUpDelivered()
    {
        Destroy(this.gameObject);
    }
}

public class PickUpData : EventArgs
{
    private PickUpMCG _senderMCG;

    public PickUpData(PickUpMCG pUMCG)
    {
        _senderMCG = pUMCG;
    }
    public PickUpMCG getSender => _senderMCG;
}
