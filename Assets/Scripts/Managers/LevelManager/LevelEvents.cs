using UnityEngine;
using System;

public static class LevelEvents
{
    public static event Action FinishLineTrigger;
    public static event Action hasNextLevelEvent;
    public static event Action hasNoNextLevelEvent;

    public static void OnFinishLineTriggered()
    {
        FinishLineTrigger?.Invoke();
    }

    public static void OnHasNextLevel()
    {
        hasNextLevelEvent?.Invoke();
    }

    public static void OnHasNoNextLevel()
    {
        hasNoNextLevelEvent?.Invoke();
    }
}
