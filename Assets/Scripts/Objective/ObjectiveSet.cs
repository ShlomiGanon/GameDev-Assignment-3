using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSet : Objective
{
    [SerializeField] private List<Objective> objectivesToTrack = new();//for inspector view
    private HashSet<Objective> objectivesNeedToComplete = new();
    private HashSet<Objective> objectivesCompleted = new();
    public event Action<Objective> OnObjectiveChanged;
    public enum TrackMode
    {
        All,
        Mandatory_Only
    }
    [SerializeField] private TrackMode trackMode = TrackMode.All;

    private void OnEnable()
    {
        this.CompleteEvent += OnSelfStateChanged;
        this.UnCompleteEvent += OnSelfStateChanged;
        InitializeTracker();
    }

    private void OnDisable()
    {
        UnRegisterToObjectivesEvents();
        this.CompleteEvent -= OnSelfStateChanged;
        this.UnCompleteEvent -= OnSelfStateChanged;
    }

    private void OnSelfStateChanged(Objective objective)
    {
        OnObjectiveChanged?.Invoke(objective);
    }

    private void RegisterToObjectivesEvents()
    {
        foreach (Objective objective in objectivesNeedToComplete)
        {
            objective.CompleteEvent += OnObjectiveStateChanged;
            objective.UnCompleteEvent += OnObjectiveStateChanged;
        }
        foreach (Objective objective in objectivesCompleted)
        {
            objective.CompleteEvent += OnObjectiveStateChanged;
            objective.UnCompleteEvent += OnObjectiveStateChanged;
        }
    }

    private void UnRegisterToObjectivesEvents()
    {
        foreach (Objective objective in objectivesNeedToComplete)
        {
            objective.CompleteEvent -= OnObjectiveStateChanged;
            objective.UnCompleteEvent -= OnObjectiveStateChanged;
        }
        foreach (Objective objective in objectivesCompleted)
        {
            objective.CompleteEvent -= OnObjectiveStateChanged;
            objective.UnCompleteEvent -= OnObjectiveStateChanged;
        }
    }

    public void SetObjectivesList(List<Objective> newObjectivesList)
    {
        objectivesToTrack = newObjectivesList ?? new List<Objective>();
        if (isActiveAndEnabled) InitializeTracker();
    }

    public void SetTrackMode(TrackMode trackMode)
    {
        this.trackMode = trackMode;
    }

    private void InitializeTracker()
    {
        UnRegisterToObjectivesEvents();

        objectivesNeedToComplete = new();
        objectivesCompleted = new();

        TransferFromListToHashsets();

        RegisterToObjectivesEvents();
        CheckAndUpdateCompletionStatus();
    }

    void TransferFromListToHashsets()
    {
        List<Objective> relevantObjectives = new(objectivesToTrack.Count);
        foreach (Objective obj in objectivesToTrack)
        {
            if (obj == null || obj == this) continue;

            switch (trackMode)
            {

                case TrackMode.All:
                {
                    relevantObjectives.Add(obj);
                    if (obj.IsCompleted)
                    {
                        objectivesCompleted.Add(obj);
                    }
                    else
                    {
                        objectivesNeedToComplete.Add(obj);
                    }
                    break;
                }

                case TrackMode.Mandatory_Only:
                {
                    if (obj.IsOptional)
                    {
                        //ignore optional objectives
                        continue;
                    }
                    else //is madatory
                    {
                        relevantObjectives.Add(obj);
                        if (obj.IsCompleted)
                        {
                            objectivesCompleted.Add(obj);
                        }
                        else
                        {
                            objectivesNeedToComplete.Add(obj);
                        }
                    }
                    break;
                }
            }
        }
        objectivesToTrack = relevantObjectives;
    }

    private void OnObjectiveStateChanged(Objective objective)
    {
        if (objective.IsCompleted)
        {
            objectivesNeedToComplete.Remove(objective);
            objectivesCompleted.Add(objective);
        }
        else
        {
            objectivesCompleted.Remove(objective);
            objectivesNeedToComplete.Add(objective);
        }

        CheckAndUpdateCompletionStatus();
        OnObjectiveChanged?.Invoke(objective);
    }

    public void CheckAndUpdateCompletionStatus()
    {
        bool newStatus = (objectivesNeedToComplete.Count == 0 && (objectivesNeedToComplete.Count + objectivesCompleted.Count) > 0);

        //fire the events only if the status was change.
        if (newStatus != IsCompleted)
        {
            if (newStatus) SetComplete();
            else SetUncomplete();
        }
    }

    public int GetNeedToCompleteCount()
    {
        if (objectivesNeedToComplete == null) return 0;
        return objectivesNeedToComplete.Count;
    }

    public int GetCompleteCount()
    {
        if (objectivesCompleted == null) return 0;
        return objectivesCompleted.Count;
    }
}
