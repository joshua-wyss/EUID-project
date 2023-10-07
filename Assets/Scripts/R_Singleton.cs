using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Singleton : MonoBehaviour
{
    [SerializeField] GameObject _playerGo;
    [SerializeField] InLevelUIManager _UIManager;

    public static R_Singleton Instance { get; private set; }
    private void Awake() {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    public GameObject GetPlayerGO()
    {
        return _playerGo;
    }
    public InLevelUIManager GetUIManager()
    {
        return _UIManager;
    }
}
