using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameStateManager : MonoBehaviour
{
    List<GameState> states = new();
    [field: SerializeField] public GameState CurrentState { get;private set; }
    [field: SerializeField] public GameState PreviousState { get; set; }
    [SerializeField] GameState defaultState; //starting state or if the real current state is null

    bool justChangedState = false;

    private void Awake()
    {
        states.AddRange(GetComponentsInChildren<GameState>());
        //TODO: subscribes to events

        if(CurrentState == null)
        {
            ChangeState(defaultState);
        }
    }

    private void ChangeState(GameState newState)
    {
        if (newState == null)
        {
            Debug.LogError($"{nameof(newState)} was null");
        }

        if (newState == CurrentState)
            return;

        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();

        justChangedState = true;
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
    public enum GameStates
    {
        In
    }
}
