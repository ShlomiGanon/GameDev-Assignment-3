using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor;

public static class LevelEvents
{
    public static event Action FinishLineTrigger;
    public static event Action hasNextLevelEvent;
    public static event Action hasNoNextLevelEvent;
    public static event Action loadNextLevel;
    public static event Action checkForNextLevel;
    public static event Action levelFailed;

    public static event Action restartLevelEvent;
    public static event Action restartedLevelEvent;

    public static void OnFinishLineTriggered()
    {
        FinishLineTrigger?.Invoke();
    }
    public static void OnRestartLevel()
    {
        restartLevelEvent?.Invoke();
    }
    public static void OnRestartedLevel()
    {
        restartedLevelEvent?.Invoke();
    }
    public static void OnLevelFailed()
    {
        levelFailed?.Invoke();
    }

    public static void OnHasNextLevel()
    {
        hasNextLevelEvent?.Invoke();
    }

    public static void OnHasNoNextLevel()
    {
        hasNoNextLevelEvent?.Invoke();
    }

    public static void OnLoadNextLevel()
    {
        loadNextLevel?.Invoke();
    }

    public static void OnCheckForNextLevel()
    {
        checkForNextLevel?.Invoke();
    }
}
