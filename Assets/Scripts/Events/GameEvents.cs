using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<GameObject, bool> ButtonChangeStatus;
    public static Action GameEnd;

    public static void OnGameEnd()
    {
        Debug.Log("Game End");
        GameEnd?.Invoke();
    }
}
