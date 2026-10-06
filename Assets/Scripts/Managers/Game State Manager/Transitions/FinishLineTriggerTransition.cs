using UnityEngine;

public class FinishLineTriggerTransition : TransitionBase
{
    private bool canTransition = false;

    private void OnEnable()
    {
        LevelEvents.FinishLineTrigger += CanTransitionUpdate;
    }

    private void OnDisable()
    {
        LevelEvents.FinishLineTrigger -= CanTransitionUpdate;
    }

    private void CanTransitionUpdate()
    {
        canTransition = true;
    }

    public override void ResetParameters()
    {
        canTransition = false;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = canTransition;
        ResetParameters();
        return shouldTransition;
    }
}
