using UnityEngine;

public class HasNextLevelTransition : TransitionBase
{
    private bool canTransition = false;

    private void OnEnable()
    {
        LevelEvents.hasNextLevelEvent += CanTransitionUpdate;
    }

    private void OnDisable()
    {
        LevelEvents.hasNextLevelEvent -= CanTransitionUpdate;
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
