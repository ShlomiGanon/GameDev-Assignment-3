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

    public void SetMovement(Vector2 direction)
    {
        animator.SetFloat(nameof(AnimationParameters.Speed), Mathf.Abs(direction.x));
        FlipSprite(direction.x);
    }
    public void PlayJump()
    {
        animator.SetTrigger(nameof(AnimationParameters.Jump));
    }
   
    public void PlayDeath()
    {
        animator.SetTrigger(nameof(AnimationParameters.Death));
    }
    public void PlayHit()
    {
        animator.SetTrigger(nameof(AnimationParameters.TakeHit));
    }
    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool(nameof(AnimationParameters.IsGrounded), isGrounded);
    }
    public void SetVerticalVelocity(float velocityY)
    {
        animator.SetFloat(nameof(AnimationParameters.VelocityY), velocityY);
    }
    public void FlipSprite(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))return;

        bool facingLeft = directionX < 0;
        spriteRenderer.flipX = facingLeft;
    }
    enum AnimationParameters
    {
        Jump,
        VelocityY,
        Speed,
        IsGrounded,
        Death,
        TakeHit
    }
}
