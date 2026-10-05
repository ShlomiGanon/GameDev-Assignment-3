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
    [SerializeField] private UnityEvent additionalStartEvent;
    [SerializeField] private UnityEvent additionalCompleteEvent;
    [SerializeField] private UnityEvent additionalUnCompleteEvent;
    //instance events (to be able to add on inspector)
    public event Action<Objective> StartEvent;
    public event Action<Objective> CompleteEvent;
    public event Action<Objective> UnCompleteEvent;

    private void Start()
    {
        if (TriggerUncompleteOnStart) 
        {
            SetUncomplete();
        }
        else
        {
            SetAsNotCompleted();
            StartEvent?.Invoke(this);
            additionalStartEvent?.Invoke();
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
        CompleteEvent?.Invoke(this);
        additionalCompleteEvent?.Invoke();
    }


    public void SetUncomplete()
    {
        SetAsNotCompleted();
        UnCompleteEvent?.Invoke(this);
        additionalUnCompleteEvent?.Invoke();
    }

}
