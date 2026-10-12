using UnityEngine;

public class FailedTrigger : MonoBehaviour
{
    public void TriggerFailedEvent()
    {
        LevelEvents.OnLevelFailed();
    }
}
