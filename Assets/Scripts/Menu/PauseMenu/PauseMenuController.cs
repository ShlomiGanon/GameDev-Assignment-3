using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        GameStateEvents.StateUpdated += OnStateUpdated;
    }

    private void OnStateUpdated(StateSO state)
    {
        pausePanel.SetActive(state.ShowPauseMenu);
    }

    private void OnDestroy()
    {
        GameStateEvents.StateUpdated -= OnStateUpdated;
    }
}