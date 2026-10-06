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
        Debug.Log("FINISH LINE EVENT RECEIVED");
        canTransition = true;
    }

    public override void ResetParameters()
    {
        canTransition = false;
    }

    public override bool ShouldTransition()
    {
        if (canTransition)
            Debug.Log("FINISH LINE TRANSITION = TRUE");

        bool shouldTransition = canTransition;
        ResetParameters();
        return shouldTransition;
    }
}
