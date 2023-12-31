using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OverLayM : MonoBehaviour
{
    private const string ChargeBarName = "ReloadBar";
    private const string HealthBarName = "HealthBar";
    private const string ShieldBarName = "ShieldBar";
    private const string DisplayLabelName = "CollectionDisplay";

    [SerializeField] PlayerWeaponManager pWep;
    [SerializeField] PlayerHealthManager _playerHealthManager;
    private ProgressBar _charegeBar;
    private ProgressBar _healthBar;
    private ProgressBar _shieldBar;
    private Label _displayLabel;
    private UIDocument _overlayUIDoc;
    private void Awake() {
        GameObject playerGo = R_Singleton.Instance.GetPlayerGO();
        pWep = playerGo.GetComponent<PlayerWeaponManager>();
        _playerHealthManager = playerGo.GetComponent<PlayerHealthManager>();
    }
    private void OnEnable() {
        _overlayUIDoc = GetComponent<UIDocument>();
        _charegeBar = _overlayUIDoc.rootVisualElement.Q<ProgressBar>(ChargeBarName);
        _healthBar = _overlayUIDoc.rootVisualElement.Q<ProgressBar>(HealthBarName);
        _shieldBar = _overlayUIDoc.rootVisualElement.Q<ProgressBar>(ShieldBarName);

        _displayLabel = _overlayUIDoc.rootVisualElement.Q<Label>(DisplayLabelName);

        _playerHealthManager.OnChangeHealth += OnRecieveHealthChange;
        _playerHealthManager.OnChangeShield += OnRecieveShieldChange;
        pWep.OnChangeCharge += OnRecieveChargeChange;

        _healthBar.value = _playerHealthManager.HealthPercent;
        _shieldBar.value = _playerHealthManager.ShieldPercent;
    }
    private void OnRecieveHealthChange(object sender, float pHealth)
    {
        _healthBar.value = pHealth;
    }
    private void OnRecieveShieldChange(object sender, float pShield)
    {
        _shieldBar.value = pShield;
    }
    private void OnRecieveChargeChange(object sender, float pCharge)
    {//WIP once the player has a charge and it's not just the weapon.
        _charegeBar.value = pCharge;
    }
    public void ChangeDisplay(String s)
    {
        _displayLabel.text = s;
    }
}
