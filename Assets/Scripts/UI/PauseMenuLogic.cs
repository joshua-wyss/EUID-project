using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuLogic : MonoBehaviour
{
    private const string ResumeButtonName = "ResumeButton";
    private const string MenuButtonName = "MenuButton";
    private const string QuitButtonName = "QuitButton";

    private UIDocument _pauseUIDocument;

    private void OnEnable() {
        _pauseUIDocument = GetComponent<UIDocument>();

        _pauseUIDocument.rootVisualElement.Q<Button>(ResumeButtonName).clicked += () =>
        R_Singleton.Instance.GetUIManager().ExitPauseMenu();

        _pauseUIDocument.rootVisualElement.Q<Button>(MenuButtonName).clicked += () =>
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        _pauseUIDocument.rootVisualElement.Q<Button>(QuitButtonName).clicked += () =>
        {
            Debug.Log("Exit Button Clicked");
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        };
    }


}
