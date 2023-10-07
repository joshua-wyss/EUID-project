using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private const string StartButtonName = "StartButton";
    private const string ExitButtonName = "ExitButton";



    private UIDocument _mainMenuUIDocument;
    private void OnEnable() {
        _mainMenuUIDocument = GetComponent<UIDocument>();

        _mainMenuUIDocument.rootVisualElement.Q<Button>(StartButtonName).clicked += () =>
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        _mainMenuUIDocument.rootVisualElement.Q<Button>(ExitButtonName).clicked += () =>
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
