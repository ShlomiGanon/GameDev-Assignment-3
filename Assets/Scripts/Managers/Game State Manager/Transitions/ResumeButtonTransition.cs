using UnityEngine;
using UnityEngine.UI;

public class ResumeButtonTransition : TransitionBase
{
    [SerializeField] Button resumeButton;

    private bool resumePressed = false;

    protected override void Awake()
    {
        base.Awake();
        resumeButton.onClick.AddListener(OnResumePressed);
    }

    private void OnResumePressed()
    {
        resumePressed = true;
    }

    public override bool ShouldTransition()
    {
        bool shuoldTransition = resumePressed;

        resumePressed = false;

        return base.ShouldTransition() && shuoldTransition;
    }

    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(OnResumePressed);
    }
}
