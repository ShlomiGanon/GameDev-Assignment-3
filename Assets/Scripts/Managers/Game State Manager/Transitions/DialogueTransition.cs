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
            DialogueEvents.OnDialogueStart += CanTransitionUpdate;
        }
        else if(trigger == DialogueStatusTransition.dialogueEnd)
        {
            DialogueEvents.OnDialogueEnd += CanTransitionUpdate;
        }
    }

    private void OnDisable()
    {
        if (trigger == DialogueStatusTransition.dialogueStart)
        {
            DialogueEvents.OnDialogueStart -= CanTransitionUpdate;
        }
        else if (trigger == DialogueStatusTransition.dialogueEnd)
        {
            DialogueEvents.OnDialogueEnd -= CanTransitionUpdate;
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
