using System;
using UnityEngine;

public static class DialogueEvents
{
    public static event Action dialogueEnd;
    public static event Action dialogueStart;

    public static void OnDialogueEnd()
    {
        dialogueEnd?.Invoke();
    }

    public static void OnDialogueStart()
    {
        dialogueStart?.Invoke();
    }
}
