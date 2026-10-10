using UnityEngine;

public class EpsilonTransition : TransitionBase
{
    public override void ResetParameters()
    {
        
    }

    public override bool ShouldTransition()
    {
        return true;
    }
}
