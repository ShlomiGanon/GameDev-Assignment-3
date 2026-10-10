using UnityEngine;

public class ButtonPressedTransition : TransitionBase
{
    [SerializeField] GameStateButtonTransition buttonToPress;
    bool hasPressed = false;
    
    void Start()
    {
        GameStateEvents.ButtonPressed += OnButtonPressed;
    }

    private void OnButtonPressed(GameStateButtonTransition transition)
    {
        if (buttonToPress == transition)
            hasPressed = true;
    }

    public override void ResetParameters()
    {
        hasPressed = false;
    }

    public override bool ShouldTransition()
    {
        bool shouldTransition = hasPressed;
        ResetParameters();
        return shouldTransition;
    }

    private void OnDestroy()
    {
        GameStateEvents.ButtonPressed -= OnButtonPressed;
    }

}
