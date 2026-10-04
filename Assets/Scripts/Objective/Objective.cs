using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    [field: SerializeField] public bool IsCompleted { get; private set; }

    [field: SerializeField] public bool IsOptional { get;private set; }
    //(isOptional == false) -> you must complete this objective to pass the level.
    //(isOptional == true) -> you can complete the level without complete this objective.

    [field: SerializeField] public bool TriggerUncompleteOnStart { get; private set; }

    //instance events (to be able to add on inspector)
    [SerializeField] private UnityEvent startEvents;
    [SerializeField] private UnityEvent completeEvents;
    [SerializeField] private UnityEvent unCompleteEvents;

    //global objectives events (every insance fire them)

    private void Start()
    {
        SetAsNotCompleted();
        if (TriggerUncompleteOnStart) 
        {
            unCompleteEvents?.Invoke();
            ObjectivesEvents.OnObjectiveUncompleted(this);
        }
        else
        {
            startEvents?.Invoke();
            ObjectivesEvents.OnObjectiveStart(this);
        }
    }



    private void SetAsCompleted()
    {
        IsCompleted = true;
    }

    private void SetAsNotCompleted()
    {
        IsCompleted = false;
    }



    public void SetComplete()
    {
        SetAsCompleted();
        completeEvents?.Invoke();
        ObjectivesEvents.OnObjectiveCompleted(this);
    }


    public void SetUncomplete()
    {
        SetAsNotCompleted();
        unCompleteEvents?.Invoke();
        ObjectivesEvents.OnObjectiveUncompleted(this);
    }

}
