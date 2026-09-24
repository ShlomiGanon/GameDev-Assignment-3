using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 direction = Vector2.zero;
    [SerializeField] CharacterSO data;
    private bool isGrounded = false;
    private bool isPushing = false;

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
    private void CheckPushing(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Pushable"))
            return;

        if(!isGrounded)
        {
            isPushing = false;
            return;
        }

        bool pushing = false;

        foreach(ContactPoint2D contact in collision.contacts)
        {
            float directionToObject = -contact.normal.x;
            if(Mathf.Abs(directionToObject) > 0.5f && direction.x * directionToObject > 0f)
            {
                pushing = true;
                break;
            }
        }
        isPushing = pushing;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckPushing(collision);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        foreach (ContactPoint2D contact in other.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
        if(other.gameObject.CompareTag("Pushable"))
        {
            isPushing = false;
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
    public bool GetIsPushing()
    {
        return isPushing;
    }
}
