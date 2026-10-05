using UnityEngine;

public class LevelEndState : GameState
{
    [SerializeField] private LevelManager levelManager;

    public override void Enter()
    {
        base.Enter();
        levelManager.CheckForNextLevel();
    }
}
