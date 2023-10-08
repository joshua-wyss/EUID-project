using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelUIManager : MonoBehaviour
{
    [SerializeField] OverLayM overLayLogicPrefab;
    [SerializeField] PauseMenuLogic pauseMenuLogicPrefab;
    private OverLayM _overlayLogic;
    private PauseMenuLogic _pauseMenuLogic;
    private float _timeScale;

    private void Awake()
    {
        _overlayLogic = Instantiate(overLayLogicPrefab, this.transform);
        _pauseMenuLogic = Instantiate(pauseMenuLogicPrefab, this.transform);
    }
    private void Start() {
        _overlayLogic.gameObject.SetActive(true);
        _pauseMenuLogic.gameObject.SetActive(false);
    }
    public void ActivePauseMenu()
    {
        if(_overlayLogic.gameObject.activeInHierarchy)
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0f;
            _overlayLogic.gameObject.SetActive(false);
            _pauseMenuLogic.gameObject.SetActive(true);
        }
    }
    public void ExitPauseMenu()
    {
        _overlayLogic.gameObject.SetActive(true);
        _pauseMenuLogic.gameObject.SetActive(false);
        Time.timeScale = _timeScale;
    }
    public OverLayM GetOverLay()
    {
        return _overlayLogic;
    }
}
