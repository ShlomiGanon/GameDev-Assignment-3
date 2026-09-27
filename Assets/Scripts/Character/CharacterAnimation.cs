using Unity.VisualScripting;
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

    public void SetMovement(float speed)
    {
        animator.SetFloat(nameof(AnimatorParameters.Speed), Mathf.Abs(speed));
    }

    public void SetJump()
    {
        animator.SetTrigger(nameof(AnimatorParameters.Jump));
    }

    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool(nameof(AnimatorParameters.IsGrounded), isGrounded);
    }

    public void SetVerticalVelocity(float velocity)
    {
        animator.SetFloat(nameof(AnimatorParameters.VerticalVelocity), velocity);
    }

    public void SetPushing(bool isPushing)
    {
        animator.SetBool(nameof(AnimatorParameters.IsPushing), isPushing);
    }

    public void SetWin(bool isWin)
    {
        animator.SetTrigger(nameof(AnimatorParameters.Win));
    }

    public void FlipSprite(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))
            return;

        bool facingLeft = directionX < 0;
        spriteRenderer.flipX = facingLeft;
    }
    public enum AnimatorParameters 
    {
        Speed,
        Jump,
        IsGrounded,
        Win,
        IsPushing,
        VerticalVelocity
    }
  
}
