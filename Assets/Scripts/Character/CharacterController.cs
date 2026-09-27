using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;

    private bool enableControll = true;
    private bool isPushing = false;
    private bool lastGrounded;
    private Vector2 directionX = Vector2.zero;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        if(characterMovement == null)
        {
            Debug.LogError("The GameObject dosn't have characterMovement");
        }
        characterAnimation = GetComponent<CharacterAnimation>();
        if(characterAnimation == null)
        {
            Debug.LogError("The GameObject dosn't have characterAnimation");
        }
    }
    private void Update()
    {
        characterAnimation.SetMovement(characterMovement.GetHorizontalVelocity());
        characterAnimation.SetVerticalVelocity(characterMovement.GetVerticalVelocity());
        UpdateGroundedState();
    }

    public void EnableController()
    {
        enableControll = true;
    }

    public void DisableController()
    {
        enableControll = false;
        characterMovement.HandleMovement(0f);
    }

    public void SetFacing(float directionX)
    {
        if (Time.timeScale == 0f || !enableControll) return;
        characterAnimation.FlipSprite(directionX);
    }

    public void Move(Vector2 direction)
    {
        if (Time.timeScale == 0f || !enableControll) return;
        directionX = direction;

        characterMovement.HandleMovement(direction.x);
        SetFacing(direction.x);
    }

    public void TryJump()
    {
        if (Time.timeScale == 0f || !enableControll) return;
        if (characterMovement.HandleJump())
        {
            characterAnimation.SetJump();
        }
    }
    private void UpdateGroundedState()
    {
        bool currentGrounded = characterMovement.GetIsGrounded();

        if (lastGrounded == currentGrounded)
            return;

        lastGrounded = currentGrounded;
        characterAnimation.SetGrounded(currentGrounded);
    }

    private void UpdatePushingState(bool value)
    {
        if (isPushing == value)
            return;

        isPushing = value;
        characterAnimation.SetPushing(isPushing);
    }
    private void CheckPushing(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Pushable"))
            return;
       
        if (!characterMovement.GetIsGrounded())
        {
            UpdatePushingState(false);
            return;
        }

        bool pushing = false;

        foreach(ContactPoint2D contact in collision.contacts)
        {
            float directionToObject = -contact.normal.x;
            if(Math.Abs(contact.normal.x) > 0.5f && directionToObject * directionX.x > 0f)
            {
                pushing = true;
                break;
            }
        }
        UpdatePushingState(pushing);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckPushing(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Pushable"))
        {
            UpdatePushingState(false);
        }
    }

    private void OnEnable()
    {
        InputEvents.Move += Move;
        InputEvents.Jump += TryJump;
    }

    private void OnDisable()
    {
        InputEvents.Move -= Move;
        InputEvents.Jump -= TryJump;
    }
}