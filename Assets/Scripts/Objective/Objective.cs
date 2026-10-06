
using System;
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

    virtual protected void Start()
    {
        if (TriggerUncompleteOnStart) 
        {
            SetUncomplete(true);
        }
        else if(IsCompleted)
        {
            SetComplete(true);
        }
        else
        {
            StartEvent?.Invoke(this);
            additionalStartEvent?.Invoke();
        }
    }
    public void SetComplete() => SetComplete(false);
    protected void SetComplete(bool force)
    {
        if (IsCompleted && !force) return;
        IsCompleted = true;
        CompleteEvent?.Invoke(this);
        additionalCompleteEvent?.Invoke();
    }

    public void SetUncomplete() => SetUncomplete(false);
    protected void SetUncomplete(bool force)
    {
        if (!IsCompleted && !force) return;
        IsCompleted = false;
        UnCompleteEvent?.Invoke(this);
        additionalUnCompleteEvent?.Invoke();
    }

}
