using System;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backToMenu;
    [SerializeField] private Button restartButton;

    private static float GameTimeScale = 1.0f;
    private static float PauseTimeScale = 0f;

    private string menuSceneName = "MainMenu";


    private void Awake()
    {
        GameStateEvents.StateUpdated += OnStateUpdated;

        resumeButton.onClick.AddListener(OnResumeClicked);
        backToMenu.onClick.AddListener(OnMenuClicked);
        restartButton.onClick.AddListener(OnRestartClicked);

    }

    private void OnResumeClicked()
    {
        GameStateEvents.ButtonPressed?.Invoke(GameStateButtonTransition.Resume);
        HideMenu();
    }

    private void OnMenuClicked()
    {
        HideMenu();
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnRestartClicked()
    {
        GameStateEvents.ButtonPressed?.Invoke(GameStateButtonTransition.Restart);
        HideMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnStateUpdated(StateSO state)
    {
        Debug.Log("twis?");
        pausePanel.SetActive(state.ShowPauseMenu);
    }

    private void HideMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = GameTimeScale;
    }

    private void ShowMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = PauseTimeScale;
    }

    public void MenuPressed()
    {
        if (gameObject.activeSelf)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }
    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(OnResumeClicked);
        backToMenu.onClick.RemoveListener(OnMenuClicked);
        restartButton.onClick.RemoveListener(OnRestartClicked);

        GameStateEvents.StateUpdated -= OnStateUpdated;
    }
}