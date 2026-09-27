using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 direction = Vector2.zero;
    [SerializeField] CharacterSO data;

    private float minGroundNormalY;
    private bool isGrounded = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (rb2d == null)
        {
            Debug.LogError("CharacterMovement: Rigidbody2D not found!");
            enabled = false;
        }
    }

    public void HandleMovement(float directionX)
    {
        direction.x = directionX;
    }

    public bool HandleJump()
    {
        if (!isGrounded)
        {
            return false; 
        }
        isGrounded = false;

        rb2d.AddForceY(data.JumpForce, ForceMode2D.Impulse);
        return true;
    }
    private void FixedUpdate()
    {
        rb2d.linearVelocityX = direction.x * data.MoveSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.normal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    public float GetHorizontalVelocity()
    {
        return rb2d.linearVelocityX;
    }
    public float GetVerticalVelocity()
    {
        return rb2d.linearVelocityY;
    }
    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
