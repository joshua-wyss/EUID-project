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
    [SerializeField] Material _shieldMat;
    [SerializeField] Vector3 ShieldBlue = new Vector3(9, 100, 125);
    [SerializeField] Vector3 ShieldRed = new Vector3(138,0,8);

    private void Awake() {
        PlayerHealthManager pHM = GetComponent<PlayerHealthManager>();
        pHM.OnChangeShield += OnShieldChangeRecieved;
        pHM.OnShieldBreakFull += OnShieldBrokenRecieved;
        ShieldObject.SetActive(false);
        _shieldMat = ShieldObject.GetComponent<Renderer>().material;
    }
    private void OnShieldChangeRecieved(object sender, float percentage)
    {
        //Debug.Log(percentage);
        if(_ShieldsActive)
            ShieldIsHit(percentage/100);
    }
    private void OnShieldBrokenRecieved(object sender, bool inactive)
    {
        _ShieldsActive = !inactive;
    }
    private void FixedUpdate() {
        _Timer -= Time.deltaTime;
        if(_Timer < 0){
            ShieldObject.SetActive(false);
            _Timer = 0;
            //this.enabled = false;
        }
    }
    public void ShieldIsHit(float p)
    {
        _Timer = ShieldFLickerTime;
        Vector3 shieldColorV3 = Vector3.Lerp(ShieldRed, ShieldBlue, p);
        //Debug.Log(shieldColorV3);
        Color shieldColor = new Color(shieldColorV3.x/255, shieldColorV3.y/255, shieldColorV3.z/255);
        _shieldMat.SetColor("_EmissionColor", shieldColor);
        ShieldObject.GetComponent<Renderer>().material = _shieldMat;
        ShieldObject.SetActive(true);
    }
}
