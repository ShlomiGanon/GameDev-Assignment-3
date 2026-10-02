using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePressedTransition : TransitionBase
{
    bool pausePressed = false;
    private string[] menuSceneNames = { "MainMenu" };

    private void Start()
    {
        InputEvents.Pause += OnPausePressed;
    }

    private void OnPausePressed()
    {
        pausePressed = true;
    }
    
    public override bool ShouldTransition()
    {
        bool shouldTransition = pausePressed && !menuSceneNames.Contains(SceneManager.GetActiveScene().name);
        pausePressed = false;
        return base.ShouldTransition() && shouldTransition;
    }

    private void OnDestroy()
    {
        InputEvents.Pause -= OnPausePressed;
    }
}
