using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        GameStateEvents.StateUpdated += OnStateUpdated;
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
        pausePanel.SetActive(state.ShowPauseMenu);
    }

    private void OnDestroy()
    {
        GameStateEvents.StateUpdated -= OnStateUpdated;
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
        if(continueButton != null) continueButton.onClick.RemoveListener(OnContinueClicked);
        if(restartButton != null) restartButton.onClick.RemoveListener(OnRestartClicked);
        if (backToMenuButton != null) backToMenuButton.onClick.RemoveListener(OnMenuClicked);
        Time.timeScale = 1f;//to continue if the scene was destroyed
    }
}