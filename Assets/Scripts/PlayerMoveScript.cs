using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] float ignoreRadius;
    [SerializeField] Vector2 screenCenter;
    [SerializeField] float sensitivity = 100;
    [SerializeField] float rollForce = 200;
    [SerializeField] Vector2 wasdDirection;
    [SerializeField] float thrust = 15000;
    [SerializeField] int thrustSetting = 1;
    [SerializeField] Vector2 MinMaxThrust = new Vector2 (-1, 10);
    [SerializeField] float Speed = 0;
    [SerializeField] Rigidbody rigi;

    private void Awake() {
        rigi = GetComponent<Rigidbody>();
        screenCenter = new Vector2 (Screen.width, Screen.height)/2;
    }
    public void ListenToTestGates(List<SpeedUpGate> list) {
        foreach (SpeedUpGate item in list)
        {
            item.OnEnterdSpeedUp += OnEnterSpeedUp;            
        }
    }
    private void OnEnterSpeedUp(object sender, SpeedUpData SUD)
    {
        StartCoroutine(SpeedBoost(SUD.Force, SUD.Duration));
    }
    private void FixedUpdate() {
        
        Vector2 mp = Input.mousePosition;
        float pitch = -(mp.y - screenCenter.y);
        float yaw = mp.x - screenCenter.x;
        

        rigi.AddRelativeTorque(pitch * sensitivity * Time.deltaTime, yaw * sensitivity * Time.deltaTime, Time.deltaTime * rollForce * -wasdDirection.x);
        rigi.AddRelativeForce(0f, 0f, thrust * thrustSetting * Time.deltaTime);
        Speed = rigi.velocity.magnitude;
    }
    public void OnMove(InputValue inputValue)
    {
        wasdDirection = inputValue.Get<Vector2>();
        thrustSetting += (int)inputValue.Get<Vector2>().y;
        if(thrustSetting > MinMaxThrust.y) thrustSetting = (int)MinMaxThrust.y;
        if(thrustSetting < MinMaxThrust.x) thrustSetting = (int)MinMaxThrust.x;
    }
    public IEnumerator SpeedBoost(int force, float duration)
    {
        for(float f = 0; f < duration; f++)
        {
            rigi.AddRelativeForce(0f, 0f, force);
            Debug.Log("SpeedBoost apply");
            yield return new WaitForFixedUpdate();
        }
    }
    public void OnExit(InputValue inputValue)
    {
        R_Singleton.Instance.GetUIManager().ActivePauseMenu();
    }
}
