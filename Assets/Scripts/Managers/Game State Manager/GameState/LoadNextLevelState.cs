using UnityEngine;

public class LoadNextLevelState : GameState
{
    public override void Enter()
    {
        base.Enter();

        LevelEvents.OnLoadNextLevel();
    }
}
