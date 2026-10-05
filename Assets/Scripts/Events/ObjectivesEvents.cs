using System;

public static class ObjectivesEvents
{
    public static event Action ObjectivesCompleted;
    public static event Action ObjectivesIncompleted;

    public static void OnObjectivesCompleted()
    {
        ObjectivesCompleted?.Invoke();
    }

    public static void OnObjectivesIncompleted()
    {
        ObjectivesIncompleted?.Invoke();
    }
}
