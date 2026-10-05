using System;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite releasedSprite;

    [SerializeField] private bool playerCanPress = true;
    [SerializeField] private bool pushableCanPress = true;

    private readonly HashSet<Collider2D> objectsOnPlate = new();

    public event Action<bool> OnPressedChanged;

    public bool IsPressed { get; private set; }

    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = releasedSprite;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanPress(other))
            return;

        objectsOnPlate.Add(other);
        UpdatePlateState();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (objectsOnPlate.Remove(other))
            UpdatePlateState();
    }

    private bool CanPress(Collider2D other)
    {
        if (playerCanPress && other.gameObject.CompareTag("Player"))
            return true;
        
        if (pushableCanPress && other.gameObject.CompareTag("Pushable"))
            return true;

        return false;
    }

    private void UpdatePlateState()
    {
        bool pressed = objectsOnPlate.Count > 0;

        if (IsPressed == pressed)
            return;

        IsPressed = pressed;

        spriteRenderer.sprite = IsPressed ? pressedSprite : releasedSprite;

        OnPressedChanged?.Invoke(IsPressed);
    }
}
