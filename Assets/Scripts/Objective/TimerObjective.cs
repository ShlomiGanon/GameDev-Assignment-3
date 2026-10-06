using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerObjective : Objective
{
    [SerializeField] private bool isStartPositive = true;
    [SerializeField] protected float startDelaySeconds = 15f;
    [SerializeField] protected float flipInSeconds = 15f;
    [SerializeField] protected float secondsLeft;
    [SerializeField] protected Coroutine coroutine;

    [SerializeField] private UnityEvent<float> additionalTickEvent;
    public event Action<Objective, float> TickEvent;

    private bool isInitialized = false;

    protected override void Start()
    {
        base.Start();
        isInitialized = true;
        secondsLeft = flipInSeconds;
        StartTimer();
    }

    protected void OnEnable()
    {
        if (isInitialized && secondsLeft > 0)
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
        if (secondsLeft == flipInSeconds)//in the first count only
        {

            yield return new WaitForSeconds(startDelaySeconds);

            if (isStartPositive)
            {
                SetComplete();
            }
            else
            {
                SetUncomplete();
            }

        }


        while (secondsLeft > 0)
        {
            additionalTickEvent?.Invoke(secondsLeft);
            TickEvent?.Invoke(this, secondsLeft);
            if (secondsLeft < 1)
            {
                yield return new WaitForSeconds(secondsLeft);
                secondsLeft = 0;
            }
            else
            {
                secondsLeft--;
                yield return new WaitForSeconds(1f);//wait one second
            }

        }

        if (IsCompleted)
        {
            SetUncomplete();
        }
        else
        {
            SetComplete();
        }

        //invoke the time is finish (0 - seconds left)
        additionalTickEvent?.Invoke(0f);
        TickEvent?.Invoke(this, 0f);

        coroutine = null;
    }
}