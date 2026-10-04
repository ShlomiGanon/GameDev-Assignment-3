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
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowMenu()
    {
        pauseMenuPanel.SetActive(true);
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
        continueButton.onClick.AddListener(OnContinueClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        backToMenuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnDisable()
    {
        GameStateEvents.StateUpdated -= OnStateUpdated;
        continueButton.onClick.RemoveListener(OnContinueClicked);
        restartButton.onClick.RemoveListener(OnRestartClicked);
        backToMenuButton.onClick.RemoveListener(OnMenuClicked);
    }
}