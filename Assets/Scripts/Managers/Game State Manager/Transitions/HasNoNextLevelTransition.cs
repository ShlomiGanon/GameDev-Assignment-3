using UnityEngine;

public class HasNoNextLevelTransition : TransitionBase
{
    private bool canTransition = false;

    private void OnEnable()
    {
        LevelEvents.hasNoNextLevelEvent += CanTransitionUpdate;
    }

    private void OnDisable()
    {
        LevelEvents.hasNoNextLevelEvent -= CanTransitionUpdate;
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
