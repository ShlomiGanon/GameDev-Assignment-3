using System;
using UnityEngine;

public static class GameStateEvents
{
    public static Action<StateSO> StateUpdated;

    public static Func<StateSO> GetCurrentState;

    public static Action<GameStateButtonTransition> ButtonPressed;
}
