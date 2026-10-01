using UnityEngine;

public class PausePressedTransition : TransitionBase
{
    bool pausePressed = false;

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
        bool shouldTransition = pausePressed;
        pausePressed = false;
        return base.ShouldTransition() && shouldTransition;
    }

    private void OnDestroy()
    {
        InputEvents.Pause -= OnPausePressed;
    }
}
