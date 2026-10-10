using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSet : Objective
{
    public enum ObjectiveFilterMode
    {
        All,
        MandatoryOnly,
        OptionalOnly
    }

    [Header("Tracking")]
    [SerializeField] private List<Objective> objectivesToTrack = new();//for inspector view

    [Header("Filtering")]
    [SerializeField] private ObjectiveFilterMode filterMode = ObjectiveFilterMode.All;

    private readonly HashSet<Objective> pendingObjectives = new();
    private readonly HashSet<Objective> completedObjectives = new();

    public event Action<Objective> OnInnerObjectiveChanged;
    public event Action<ObjectiveSet> OnObjectiveSetChanged;

    private void OnEnable()
    {
        CompleteEvent += HandleSelfStateChanged;
        UnCompleteEvent += HandleSelfStateChanged;
        FailEvent += HandleSelfStateChanged;
        RebuildTracking();
    }

    private void OnDisable()
    {
        UnsubscribeFromTrackedObjectives();
        CompleteEvent -= HandleSelfStateChanged;
        UnCompleteEvent -= HandleSelfStateChanged;
        FailEvent -= HandleSelfStateChanged;
    }

    public void SetObjectivesList(List<Objective> newObjectives)
    {
        if (!isActiveAndEnabled)
        {
            objectivesToTrack = newObjectives != null ? new List<Objective>(newObjectives) : new List<Objective>();
            return;
        }

        ResetTracking();
        AddObjectives(newObjectives);
    }

    public void SetTrackMode(ObjectiveFilterMode trackMode)
    {
        filterMode = trackMode;
    }

    public void AddObjective(Objective objective)
    {
        TrackObjective(objective);
        CheckAndUpdateCompletionStatus();
    }

    public void AddObjectives(IEnumerable<Objective> objectives)
    {
        if (objectives == null)
        {
            return;
        }

        foreach (Objective objective in objectives)
        {
            TrackObjective(objective);
        }

        CheckAndUpdateCompletionStatus();
    }

    public void CheckAndUpdateCompletionStatus()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        bool isNowCompleted = pendingObjectives.Count == 0 && completedObjectives.Count > 0;

        //fire the events only if the status was change.
        if (isNowCompleted == IsCompleted)
        {
            return;
        }

        if (isNowCompleted)
        {
            SetComplete();
        }
        else
        {
            SetUncomplete();
        }
    }

    public int GetNeedToCompleteCount()
    {
        return pendingObjectives.Count;
    }

    public int GetCompleteCount()
    {
        return completedObjectives.Count;
    }

    private void RebuildTracking()
    {
        List<Objective> sourceObjectives = new(objectivesToTrack);

        ResetTracking();
        AddObjectives(sourceObjectives);
    }

    private void ResetTracking()
    {
        UnsubscribeFromTrackedObjectives();

        pendingObjectives.Clear();
        completedObjectives.Clear();
        objectivesToTrack.Clear();
    }

    private void TrackObjective(Objective objective)
    {
        if (objective == null || objective == this)
        {
            return;
        }

        if (!isActiveAndEnabled)
        {
            // Filtering and subscribing happen in OnEnable.
            if (!objectivesToTrack.Contains(objective))
            {
                objectivesToTrack.Add(objective);
            }

            return;
        }

        if (pendingObjectives.Contains(objective) || completedObjectives.Contains(objective))
        {
            return;
        }

        switch (filterMode)
        {
            case ObjectiveFilterMode.All:
                {
                    StartTrackingObjective(objective);
                    break;
                }

            case ObjectiveFilterMode.MandatoryOnly:
                {
                    if (objective.IsOptional)
                    {
                        //ignore optional objectives
                    }
                    else //is madatory
                    {
                        StartTrackingObjective(objective);
                    }

                    break;
                }

            case ObjectiveFilterMode.OptionalOnly:
                {
                    if (objective.IsOptional)
                    {
                        StartTrackingObjective(objective);
                    }
                    else //is mandatory
                    {
                        //ignore mandatory objectives
                    }

                    break;
                }
        }
    }

    private void StartTrackingObjective(Objective objective)
    {
        objectivesToTrack.Add(objective);

        if (objective.IsCompleted)
        {
            completedObjectives.Add(objective);
        }
        else
        {
            pendingObjectives.Add(objective);
        }

        SubscribeToObjective(objective);
    }

    private void UnsubscribeFromTrackedObjectives()
    {
        foreach (Objective objective in objectivesToTrack)
        {
            if (objective == null)
            {
                continue;
            }

            UnsubscribeFromObjective(objective);
        }
    }

    private void SubscribeToObjective(Objective objective)
    {
        objective.CompleteEvent += HandleTrackedObjectiveStateChanged;
        objective.UnCompleteEvent += HandleTrackedObjectiveStateChanged;
        objective.FailEvent += HandleTrackedObjectiveStateChanged;
    }

    private void UnsubscribeFromObjective(Objective objective)
    {
        objective.CompleteEvent -= HandleTrackedObjectiveStateChanged;
        objective.UnCompleteEvent -= HandleTrackedObjectiveStateChanged;
        objective.FailEvent -= HandleTrackedObjectiveStateChanged;
    }

    private void HandleSelfStateChanged(Objective changedObjective)
    {
        OnObjectiveSetChanged?.Invoke(this);
    }

    private void HandleTrackedObjectiveStateChanged(Objective changedObjective)
    {
        if (changedObjective.IsCompleted)
        {
            pendingObjectives.Remove(changedObjective);
            completedObjectives.Add(changedObjective);
        }
        else
        {
            completedObjectives.Remove(changedObjective);
            pendingObjectives.Add(changedObjective);
        }
        OnInnerObjectiveChanged?.Invoke(changedObjective);
        if (changedObjective.IsFail)
        {
            SetFail();
            return;
        }
        CheckAndUpdateCompletionStatus();
    }
}