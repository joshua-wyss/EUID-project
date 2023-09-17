using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OverLayM : MonoBehaviour
{
    private const string ChargeBarName = "ReloadBar";

    [SerializeField] PlayerWeaponManager pWep;
    private ProgressBar progressBar;
    private UIDocument _overlayUIDoc;
    private void OnEnable() {
        _overlayUIDoc = GetComponent<UIDocument>();
        progressBar = _overlayUIDoc.rootVisualElement.Q<ProgressBar>();
    }
    private void FixedUpdate() {
        progressBar.value = pWep.GetChargeP();
    }
}
