using System;
using UnityEngine;

public static class ObjectivesEvents
{
    public static event Action objectivesCompleted;
    public static event Action objectivesIncompleted;

    public static void OnObjectivesCompleted()
    {
        objectivesCompleted?.Invoke();
    }

    public static void OnObjectivesIncompleted()
    {
        objectivesIncompleted?.Invoke();
    }
}
