using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldAnimationHandler : MonoBehaviour
{
    [SerializeField] float _Timer;
    [SerializeField] GameObject ShieldObject;
    [SerializeField] float ShieldFLickerTime = .5f;
    [SerializeField] bool _ShieldsActive = true;

    private void Awake() {
        PlayerHealthManager pHM = GetComponent<PlayerHealthManager>();
        pHM.OnChangeShield += OnShieldChangeRecieved;
        pHM.OnShieldBreakFull += OnShieldBrokenRecieved;
        ShieldObject.SetActive(false);
    }
    private void OnShieldChangeRecieved(object sender, float percentage)
    {
        if(_ShieldsActive)
            ShieldIsHit();
    }
    private void OnShieldBrokenRecieved(object sender, bool inactive)
    {
        _ShieldsActive = !inactive;
    }
    private void FixedUpdate() {
        _Timer -= Time.deltaTime;
        Debug.Log(_Timer);
        if(_Timer < 0){
            ShieldObject.SetActive(false);
            _Timer = 0;
            //this.enabled = false;
        }
    }
    public void ShieldIsHit()
    {
        _Timer = ShieldFLickerTime;
        ShieldObject.SetActive(true);
        Debug.Log("revieved shiedl is hit");
    }
}
