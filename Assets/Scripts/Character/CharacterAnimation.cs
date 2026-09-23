using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
  
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("the GameObject dont have Animator!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("the GameObject dont have Sprite Renderer , thats wired!");
        }
    }

    public void FlipSprite(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))return;

        bool facingLeft = directionX < 0;
        spriteRenderer.flipX = facingLeft;
    }
  
}
