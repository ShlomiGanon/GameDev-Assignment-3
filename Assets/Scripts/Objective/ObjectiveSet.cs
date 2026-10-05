using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSet : Objective
{
    [SerializeField] private List<Objective> objectivesToTrack = new();//for inspector view
    private readonly HashSet<Objective> objectivesNeedToComplete = new();
    private readonly HashSet<Objective> objectivesCompleted = new();
    public event Action<Objective> OnObjectiveChanged;
    public enum TrackMode
    {
        All,
        Mandatory_Only
    }
    [SerializeField] private TrackMode trackMode = TrackMode.All;

    private void OnEnable()
    {
        InitializeTracker();
        this.CompleteEvent += OnObjectiveChanged;
        this.UnCompleteEvent += OnObjectiveChanged;
    }

    private void OnDisable()
    {
        UnRegisterToObjectivesEvents();
        this.CompleteEvent -= OnObjectiveChanged;
        this.UnCompleteEvent -= OnObjectiveChanged;
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
        objectivesToTrack = newObjectivesList;
        InitializeTracker();
    }

    public void SetTrackMode(TrackMode trackMode)
    {
        this.trackMode = trackMode;
    }

    private void InitializeTracker()
    {
        UnRegisterToObjectivesEvents();

        objectivesNeedToComplete.Clear();
        objectivesCompleted.Clear();

        TransferFromListToHashsets();

        RegisterToObjectivesEvents();
        CheckAndUpdateCompletionStatus();
    }

    void TransferFromListToHashsets()
    {
        List<Objective> relevantObjectives = new(objectivesToTrack.Count);
        foreach (Objective obj in objectivesToTrack)
        {
            if (obj == null) continue;

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
        if (objectivesNeedToComplete.Contains(objective))
        {
            objectivesNeedToComplete.Remove(objective);
            objectivesCompleted.Add(objective);
        }
        else if (objectivesCompleted.Contains(objective))
        {
            objectivesNeedToComplete.Add(objective);
            objectivesCompleted.Remove(objective);
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
}
