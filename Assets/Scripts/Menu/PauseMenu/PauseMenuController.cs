using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    [SerializeField] Button continueButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button backToMenuButton;
    string menuSceneName = "MainMenu";

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

    private void OnContinueClicked()
    {
        GameStateEvents.ButtonPressed?.Invoke(GameStateButtonTransition.Resume);
        HideMenu();
    }

    private void HideMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowMenu()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnStateUpdated(StateSO state)
    {
        if (state.ShowMenu)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }
    }

    private void OnEnable()
    {
        GameStateEvents.StateUpdated += OnStateUpdated;
        if (continueButton != null) continueButton.onClick.AddListener(OnContinueClicked);
        if (restartButton != null) restartButton.onClick.AddListener(OnRestartClicked);
        if (backToMenuButton != null) backToMenuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnDisable()
    {
        GameStateEvents.StateUpdated -= OnStateUpdated;
        if (continueButton != null) continueButton.onClick.RemoveListener(OnContinueClicked);
        if (restartButton != null) restartButton.onClick.RemoveListener(OnRestartClicked);
        if (backToMenuButton != null) backToMenuButton.onClick.RemoveListener(OnMenuClicked);
        Time.timeScale = 1f;//to continue if the scene was destroyed
    }
}