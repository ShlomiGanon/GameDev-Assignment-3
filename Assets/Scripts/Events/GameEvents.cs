using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<GameObject, bool> ButtonChangeStatus;
    public static Action GameEnd;
    public static Action DialogueStart;
    public static Action DialogueEnd;

    public static void OnGameEnd()
    {
        Debug.Log("Game End");
        GameEnd?.Invoke();
    }
    public static void OnDialogueStart()
    {
        DialogueStart?.Invoke();
    }
    public static void OnDialogueEnd()
    {
        DialogueEnd?.Invoke();
    }
}
