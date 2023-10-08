using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] List<PickUpMCG> _MCGinLevel = new List<PickUpMCG>();
    [SerializeField] int _aqquiredMCGs = 0;
    [SerializeField] int _totalMCGs = 0;

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
    }
    public int aqquiredMCGs => _aqquiredMCGs;
    public int globalMCGs => _totalMCGs;
}
