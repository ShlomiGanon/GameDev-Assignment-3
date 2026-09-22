using UnityEngine;

public class ExampleLight : MonoBehaviour
{
    [SerializeField] private Color onColor = Color.green;
    [SerializeField] private Color offColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing in this GameObject!");
        }
    }
    public void LightOn()
    {
        if(spriteRenderer != null) spriteRenderer.color = onColor;
    }

    public void LightOff()
    {
        if (spriteRenderer != null) spriteRenderer.color = offColor;
    }
}
