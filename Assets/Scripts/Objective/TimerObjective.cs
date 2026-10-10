using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerObjective : Objective
{
    [Header("Timer Settings")]
    [SerializeField] private bool isStartPositive = true;
    [SerializeField] protected float startDelaySeconds = 15f;
    [SerializeField] protected float flipInSeconds = 15f;
    [SerializeField, Min(0.01f)] private float tickIntervalSeconds = 1f;

    [Header("Timer State (Runtime)")]
    [SerializeField] protected float secondsLeft;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> additionalTickEvent;

    public event Action<TimerObjective, float> TickEvent;

    private const float TimerFinishedSeconds = 0f;

    private bool hasStartDelayPassed;
    private bool isInitialized = false;
    protected Coroutine coroutine;

    protected override void Start()
    {
        base.Start();
        isInitialized = true;
        StartTimer();
    }

    protected void OnEnable()
    {
        if (isInitialized && (!hasStartDelayPassed || secondsLeft > TimerFinishedSeconds))
        {
            StartTimer();
        }
    }

    protected void OnDisable()
    {
        StopTimer();
    }

    private void StartTimer()
    {
        StopTimer();
        coroutine = StartCoroutine(StartCount());
    }

    private void StopTimer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator StartCount()
    {
        if (!hasStartDelayPassed)
        {
            yield return new WaitForSeconds(startDelaySeconds);
            secondsLeft = flipInSeconds;
            hasStartDelayPassed = true;
            if (IsFail) 
            {
                coroutine = null;
                yield break;
            }
            if (isStartPositive)
            {
                SetComplete();
            }
            else
            {
                SetUncomplete();
            }
        }


        while (secondsLeft > TimerFinishedSeconds && !IsFail)
        {
            additionalTickEvent?.Invoke(secondsLeft);
            TickEvent?.Invoke(this, secondsLeft);
            if (secondsLeft < tickIntervalSeconds)
            {
                yield return new WaitForSeconds(secondsLeft);
                secondsLeft = TimerFinishedSeconds;
            }
            else
            {
                secondsLeft -= tickIntervalSeconds;
                yield return new WaitForSeconds(tickIntervalSeconds);
            }
        }

        if (IsFail)
        {
            //if the objective is fail we cant change it state so we stop here
            coroutine = null;
            yield break;
        }

        if (IsCompleted)
        {
            SetUncomplete();
        }
        else
        {
            SetComplete();
        }

        additionalTickEvent?.Invoke(TimerFinishedSeconds);
        TickEvent?.Invoke(this, TimerFinishedSeconds);

        coroutine = null;
    }
}