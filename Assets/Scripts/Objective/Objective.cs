using System;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    [Header("State")]
    [field: SerializeField] public bool IsCompleted { get; private set; }
    [field: SerializeField] public bool IsFail { get; private set; }

    [Header("Settings")]
    [field: SerializeField] public bool IsOptional { get; private set; }
    //(IsOptional == false) -> you must complete this objective to pass the level.
    //(IsOptional == true) -> you can complete the level without complete this objective.
    [field: SerializeField] public bool FailsPermanentlyOnUncomplete { get; private set; }
    //(FailsPermanentlyOnUncomplete == false) -> when this objective becomes uncomplete, the UnCompleteEvent is fired and it can be completed again.
    //(FailsPermanentlyOnUncomplete == true) -> when this objective becomes uncomplete, it fails permanently: the FailEvent is fired instead of the UnCompleteEvent, and it can not be completed again.
    [field: SerializeField] public bool TriggerUncompleteOnStart { get; private set; }

    [Header("Events")]
    //instance events (to be able to add on inspector)
    [SerializeField] private UnityEvent additionalStartEvent;
    [SerializeField] private UnityEvent additionalCompleteEvent;
    [SerializeField] private UnityEvent additionalUnCompleteEvent;
    [SerializeField] private UnityEvent additionalFailEvent;

    //code events (to be able to subscribe from other scripts, each one is fired with this objective as the parameter)
    public event Action<Objective> StartEvent;
    public event Action<Objective> CompleteEvent;
    public event Action<Objective> UnCompleteEvent;
    public event Action<Objective> FailEvent;


    virtual protected void Start()
    {
        if (TriggerUncompleteOnStart)
        {
            SetUncomplete(true);
        }
        else if (IsCompleted)
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
        if ((IsCompleted && !force) || IsFail) return;
        IsCompleted = true;
        CompleteEvent?.Invoke(this);
        additionalCompleteEvent?.Invoke();
    }

    public void SetUncomplete() => SetUncomplete(false);
    protected void SetUncomplete(bool force)
    {
        if ((!IsCompleted && !force) || IsFail) return;
        bool WasComplete = IsCompleted;
        IsCompleted = false;
        if (FailsPermanentlyOnUncomplete && WasComplete)
        {
            SetFail();
        }
        else
        {
            UnCompleteEvent?.Invoke(this);
            additionalUnCompleteEvent?.Invoke();
        }
    }
    public void SetFail() => SetFail(false);
    protected void SetFail(bool force)
    {
        if (IsFail && !force) return;
        IsCompleted = false;
        IsFail = true;
        FailEvent?.Invoke(this);
        additionalFailEvent?.Invoke();
    }

}