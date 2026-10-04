using System;
using UnityEngine;

public static class ObjectivesEvents
{
    public static event Action objectivesCompleted;
    public static event Action objectivesIncompleted;
    public static event Action<Objective> objectiveStart;
    public static event Action<Objective> objectiveCompleted;
    public static event Action<Objective> objectiveUncompleted;

    public static void OnObjectivesCompleted()
    {
        objectivesCompleted?.Invoke();
    }

    public static void OnObjectivesIncompleted()
    {
        objectivesIncompleted?.Invoke();
    }


    public static void OnObjectiveStart(Objective objective)
    {
        objectiveStart?.Invoke(objective);
    }

    public static void OnObjectiveCompleted(Objective objective)
    {
        objectiveCompleted?.Invoke(objective);
    }

    public static void OnObjectiveUncompleted(Objective objective)
    {
        objectiveUncompleted?.Invoke(objective);
    }
}
