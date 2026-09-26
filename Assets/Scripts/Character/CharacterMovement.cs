using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 direction = Vector2.zero;
    [SerializeField] CharacterSO data;

    private bool isGrounded = false;
    private bool isPushing = false;
    private const float HalfSideContact = 0.5f;
    private const float MinGroundNormalY = 0.5f;

    public event Action<bool> OnGroundedChanged;
    public event Action<bool> OnPushingChanged;

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
        SetGrounded(false);

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
        Debug.Log("Pushable");
        if(!isGrounded)
        {
            SetPushing(false);
            return;
        }

        bool pushing = false;

        foreach(ContactPoint2D contact in collision.contacts)
        {
            float directionToObject = -contact.normal.x;
            if(Mathf.Abs(directionToObject) > HalfSideContact && direction.x * directionToObject > 0f)
            {
                pushing = true;
                Debug.Log("isPushing");
                break;
            }
        }
        SetPushing(pushing);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckPushing(collision);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.normal.y > MinGroundNormalY)
                {
                    SetGrounded(true);
                    return;
                }
            }
        }
        else if(other.gameObject.CompareTag("Pushable"))
        {
            SetPushing(false);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SetGrounded(false);
        }

        if (other.gameObject.CompareTag("Pushable"))
        {
            SetPushing(false);
        }
    }
    private void SetGrounded(bool value)
    {
        if (value == isGrounded)
            return;

        isGrounded = value;
        OnGroundedChanged?.Invoke(isGrounded);
    }
    private void SetPushing(bool value)
    {
        if(value == isPushing) 
            return;

        isPushing= value;
        OnPushingChanged?.Invoke(isPushing);
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
