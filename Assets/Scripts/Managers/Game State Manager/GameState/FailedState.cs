using UnityEngine;

public class FailedState : GameState
{
    public override void Enter()
    {
        base.Enter();

        LevelEvents.OnRestartLevel();
    }
}
