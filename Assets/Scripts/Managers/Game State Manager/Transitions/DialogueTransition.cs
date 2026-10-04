using Unity.VisualScripting;
using UnityEngine;

public class DialogueTransition : TransitionBase
{
    [SerializeField] DialogueStatusTransition trigger;
    private bool canTransition = false;

    private void OnEnable()
    {
        if(trigger == DialogueStatusTransition.dialogueStart)
        {
            DialogueEvents.dialogueStart += CanTransitionUpdate;
        }
        else if(trigger == DialogueStatusTransition.dialogueEnd)
        {
            DialogueEvents.dialogueEnd += CanTransitionUpdate;
        }
    }

    private void OnDisable()
    {
        if (trigger == DialogueStatusTransition.dialogueStart)
        {
            DialogueEvents.dialogueStart -= CanTransitionUpdate;
        }
        else if (trigger == DialogueStatusTransition.dialogueEnd)
        {
            DialogueEvents.dialogueEnd -= CanTransitionUpdate;
        }
    }

    private void CanTransitionUpdate()
    {
        canTransition = true;
    }

    public override void ResetParameters()
    {
        canTransition = false;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = canTransition;
        ResetParameters();
        return shouldTransition;
    }
}
