using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [field: SerializeField] public StateSO StateRules { get; private set; }

    List<TransitionBase> transitions = new();

    private void Awake()
    {
        transitions.AddRange(GetComponentsInChildren<TransitionBase>());
    }

    public virtual void Enter()
    {
        foreach (TransitionBase transition in transitions)
        {
            transition.ResetParameters();
        }
        Debug.Log(
        $"State: {name} | StateSO: {StateRules.name} | " +
        $"Manager: {GetComponentInParent<GameStateManager>().GetInstanceID()}"
);
        GameStateEvents.StateUpdated?.Invoke(StateRules);
    }

    public GameState GetNextState()
    {
        foreach (var transition in transitions)
        {
            if (!transition.ShouldTransition())
                continue;

            return transition.TargetState;
        }

        return null;
    }
}
