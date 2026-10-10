using System;

public static class ObjectivesEvents
{
    public static event Action ObjectivesCompleted;
    public static event Action ObjectivesIncompleted;
    public static event Action<ObjectiveSet> ObjectivesManagerProgressChanged;
    public static void OnObjectivesCompleted()
    {
        ObjectivesCompleted?.Invoke();
    }

    public static void OnObjectivesIncompleted()
    {
        ObjectivesIncompleted?.Invoke();
    }

    public static void OnObjectivesManagerProgressChanged(ObjectiveSet updatedObjectiveSet)
    {
        ObjectivesManagerProgressChanged?.Invoke(updatedObjectiveSet);
    }
}
