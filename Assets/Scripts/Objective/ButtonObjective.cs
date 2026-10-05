using System.Collections.Generic;
using UnityEngine;

public class ButtonObjective : Objective
{
    [SerializeField] private GameObject allowedInteractor;
    //(allowedInteractor == null) -> any touch can active the button
    //(allowedInteractor != null) -> only the touch from this gameobject can active the button
    private HashSet<GameObject> activeInteractors = new();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (allowedInteractor == null || allowedInteractor == other.gameObject)
        {
            activeInteractors.Add(other.gameObject);
        }
        UpdateCompleteStatus();
    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (allowedInteractor == null || allowedInteractor == other.gameObject)
        {
            activeInteractors.Add(other.gameObject);
        }
        UpdateCompleteStatus();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (activeInteractors.Contains(other.gameObject))
        {
            activeInteractors.Remove(other.gameObject);
        }
        UpdateCompleteStatus();
    }

    private void UpdateCompleteStatus()
    {
        if (activeInteractors.Count > 0)
        {
            if (!IsCompleted) SetComplete();
        }
        else
        {
            if (IsCompleted) SetUncomplete();
        }
    }
}
