using UnityEngine;
using UnityEngine.Events;

public class ObjectivesStatusChangedTransition : TransitionBase
{
    [SerializeField] ObjectivesStatesTransition trigger;
    private bool objectiveCompleted = false;


    private void OnEnable()
    {
        if(trigger == ObjectivesStatesTransition.ObjectivesCompleted)
        {
            ObjectivesEvents.ObjectivesCompleted += OnObjectivesCompleted;
        }
        else if(trigger == ObjectivesStatesTransition.ObjectivesUncompleted)
        {
            ObjectivesEvents.ObjectivesIncompleted += OnObjectivesCompleted;
        }
    }

    private void OnObjectivesCompleted()
    {
        objectiveCompleted = true;
    }

    public override void ResetParameters()
    {
        objectiveCompleted = false;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = objectiveCompleted;
        ResetParameters();
        return shouldTransition;
    }

    private void OnDisable()
    {
        if (trigger == ObjectivesStatesTransition.ObjectivesCompleted)
        {
            //ObjectivesEvents.objectivesCompleted -= OnObjectivesCompleted;
        }
        else if (trigger == ObjectivesStatesTransition.ObjectivesUncompleted)
        {
            //ObjectivesEvents.objectivesIncompleted -= OnObjectivesCompleted;
        }
    }
}
