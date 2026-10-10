using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameStateManager : MonoBehaviour
{
    List<GameState> states = new();
    [field: SerializeField] public GameState CurrentState { get;private set; }
    [field: SerializeField] public GameState PreviousState { get; set; }
    [SerializeField] GameState defaultState;

    bool justChangedState = false;

    private void Start()
    {
        states.AddRange(GetComponentsInChildren<GameState>());

        GameStateEvents.GetCurrentState += OnGetCurrentState;

        if(CurrentState == null)
        {
            ChangeState(defaultState);
        }
    }
    
    private StateSO OnGetCurrentState()
    {
        return CurrentState.StateRules;
    }

    private void ChangeState(GameState newState)
    {
        if (newState == null)
        {
            Debug.LogError($"{nameof(newState)} was null");
            return;
        }

        if (newState == CurrentState)
            return;

        justChangedState = true;
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();

    }

    private void Update()
    {
        if(CurrentState == null)
        {
            Debug.LogError($"{nameof(CurrentState)} was null");
            return;
        }
        if(justChangedState)
        {
            justChangedState = false;
            return;
        }

        GameState nextState = CurrentState.GetNextState();
        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }
   
    private void OnDestroy()
    {
        GameStateEvents.GetCurrentState -= OnGetCurrentState;
    }
}
