using UnityEngine;

public class LevelEndState : GameState
{
    public override void Enter()
    {
        base.Enter();
        
        LevelEvents.OnCheckForNextLevel();
    }
}
