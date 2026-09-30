using UnityEngine;

public abstract class TransitionBase : MonoBehaviour
{
    protected GameState sourceState;
    [field: SerializeField] public GameState TargetState { get; protected set; }

    protected virtual void Awake()
    {
        sourceState = GetComponentInParent<GameState>();
        if(sourceState == null )
        {
            Debug.LogError($"{nameof(sourceState)} of transition {name} is null");
        }

        if(TargetState == null )
        {
            Debug.LogError($"{nameof(TargetState)} of transition {name} is null");
        }
    }

    public virtual bool ShouldTransition()
    {
        return true;
    }

}
