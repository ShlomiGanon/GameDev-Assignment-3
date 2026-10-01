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
    
    public void Enter ()
    {
        //TODO: Invoke State updated events
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
