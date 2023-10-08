using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] List<PickUpMCG> _MCGinLevel = new List<PickUpMCG>();
    [SerializeField] int _aqquiredMCGs = 0;
    [SerializeField] int _totalMCGs = 0;
    [SerializeField] LevelExit _levelExit;
    [SerializeField] List<SpeedUpGate> _speedUpGates = new List<SpeedUpGate>();

    private void Awake() {
        var tempMCGs = FindObjectsByType<PickUpMCG>(FindObjectsSortMode.None);
        foreach (PickUpMCG item in tempMCGs)
        {
            if(!_MCGinLevel.Contains(item))            
            {
                _MCGinLevel.Add(item);
            }
        }
        _totalMCGs = _MCGinLevel.Count;
        var tempTurrets = FindObjectsByType<TurretScript>(FindObjectsSortMode.None);
        GameObject player = R_Singleton.Instance.GetPlayerGO();
        foreach (TurretScript t in tempTurrets)
        {
            t.SetTarget(player.transform);
        }

        var gates = FindObjectsByType<SpeedUpGate>(FindObjectsSortMode.None);
        player.GetComponent<PlayerMoveScript>().ListenToTestGates(gates.ToList());

        if(_levelExit == null)
        {
            _levelExit = FindAnyObjectByType<LevelExit>();
        }
        _levelExit.gameObject.SetActive(false);
        player.GetComponent<PlayerMoveScript>().SetQuestPointer(_MCGinLevel[0].transform.position);

        R_Singleton.Instance.GetUIManager().GetOverLay().ChangeDisplay(
            "Collected McGuffins: " + aqquiredMCGs + "/" + globalMCGs
        );
    }
    private void OnEnable() {
        foreach (var item in _MCGinLevel)
        {
            item.OnEnterPickUp += ReEnterPickUp;
        }
    }
    private void ReEnterPickUp(object sender, PickUpData pickUpData)
    {
        _MCGinLevel.Remove(pickUpData.getSender);
        pickedUPMCG();
        pickUpData.getSender.PickUpDelivered();
    }
    private void pickedUPMCG()
    {
        _aqquiredMCGs++;
        GameObject player = R_Singleton.Instance.GetPlayerGO();
        if(_aqquiredMCGs >= _totalMCGs)
        {
            _levelExit.gameObject.SetActive(true);
            player.GetComponent<PlayerMoveScript>().SetQuestPointer(_levelExit.transform.position);
        }
        else{
            player.GetComponent<PlayerMoveScript>().SetQuestPointer(_MCGinLevel[0].transform.position);
        }
        R_Singleton.Instance.GetUIManager().GetOverLay().ChangeDisplay(
            "Collected McGuffins: " + aqquiredMCGs + "/" + globalMCGs
        );
    }
    public int aqquiredMCGs => _aqquiredMCGs;
    public int globalMCGs => _totalMCGs;
}
