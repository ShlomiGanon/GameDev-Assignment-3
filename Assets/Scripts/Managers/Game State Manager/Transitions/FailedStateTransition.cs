using UnityEngine;

public class FailedStateTransition : TransitionBase
{
    private bool failedTransition = false;

    private void OnEnable()
    {
        LevelEvents.levelFailed += FailedTransitionUpdate;
    }

    private void OnDisable()
    {
        LevelEvents.levelFailed -= FailedTransitionUpdate;
    }

    public override void ResetParameters()
    {
        failedTransition = false;
    }

    private void FailedTransitionUpdate()
    {
        failedTransition = true;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = failedTransition;
        ResetParameters();
        return shouldTransition;
    }
}
