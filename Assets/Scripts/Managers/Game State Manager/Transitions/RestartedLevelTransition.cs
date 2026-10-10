using UnityEngine;

public class RestartedLevelTransition : TransitionBase
{
    private bool restartedTranstion = false;

    private void OnEnable()
    {
        LevelEvents.restartedLevelEvent += OnRestartedUpdate;
    }
    private void OnDisable()
    {
        LevelEvents.restartedLevelEvent -= OnRestartedUpdate;
    }

    private void OnRestartedUpdate()
    {
        restartedTranstion = true;
    }

    public override void ResetParameters()
    {
        restartedTranstion = false;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = restartedTranstion;
        ResetParameters();
        return shouldTransition;
    }
}
