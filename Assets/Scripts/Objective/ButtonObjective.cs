using System.Collections.Generic;
using UnityEngine;

public class ButtonObjective : Objective
{
    [SerializeField] private GameObject allowedInteractor;
    //(allowedInteractor == null) -> any touch can active the button
    //(allowedInteractor != null) -> only the touch from this gameobject can active the button
    private readonly HashSet<Collider2D> activeColliders = new();
    [SerializeField, Min(1)] private int skippingFrames = 100;
    private int currentFrame = 0;

    private void OnDisable()
    {
        // Unity won't call OnCollisionExit2D for a disabled button, so clear manually.
        activeColliders.Clear();

        // OnDisable also runs when the scene unloads or Play mode stops.
        // Other objects may already be destroyed, so don't fire events then.
        if (gameObject.scene.isLoaded) UpdateCompleteStatus();
    }

    private void Update()
    {
        //we chack here because we can destroy some object and he stil be register to the touch set
        if(currentFrame == 0)
        {
            UpdateCompleteStatus();
        }
        //skippingFrames cant be negative or zero because we have 'Min(1)' in the field defenetion
        currentFrame = (currentFrame + 1) % skippingFrames;
    }

    private void OnCollisionEnter2D(Collision2D other) => HandleContact(other);
    private void OnCollisionStay2D(Collision2D other) => HandleContact(other);

    private void OnCollisionExit2D(Collision2D other)
    {
        activeColliders.Remove(other.collider);
        UpdateCompleteStatus();
    }
    private void UpdateCompleteStatus()
    {
        activeColliders.RemoveWhere(c => c == null || !c.enabled || !c.gameObject.activeInHierarchy);

        if (activeColliders.Count > 0)
        {
            if (!IsCompleted) SetComplete();
        }
        else
        {
            if (IsCompleted) SetUncomplete();
        }
    }

    private bool IsAllowed(Collision2D other)
    {
        if (allowedInteractor == null) return true;

        Transform touchingTransform = other.collider.transform;
        Transform allowedTransform = allowedInteractor.transform;

        bool touchingColliderIsOnTheAllowedObject = touchingTransform == allowedTransform;
        bool touchingColliderIsOnAChildOfTheAllowedObject = touchingTransform.IsChildOf(allowedTransform);

        return touchingColliderIsOnTheAllowedObject || touchingColliderIsOnAChildOfTheAllowedObject;
    }

    private void HandleContact(Collision2D other)
    {
        if (IsAllowed(other)) activeColliders.Add(other.collider);
        UpdateCompleteStatus();
    }
}
