using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button startButton;
    private string playSceneName = "Level1";

    private void OnStartGameClicked()
    {
        SceneManager.LoadScene(playSceneName);
    }
    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStartGameClicked);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStartGameClicked);
    }
}
