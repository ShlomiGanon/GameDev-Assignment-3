using System;
using UnityEditor;
using UnityEngine;

public static class InputEvents
{
    public static Action<Vector2> Move;
    public static Action Jump;
    public static Action Pause;
}
