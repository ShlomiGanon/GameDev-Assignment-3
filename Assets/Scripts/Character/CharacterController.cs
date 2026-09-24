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
        characterAnimation.SetGrounded(characterMovement.GetIsGrounded());
        characterAnimation.SetPushing(characterMovement.GetIsPushing());
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

    public void SetFacing(float directionX)//check later if neccecary
    {
        if (Time.timeScale == 0f || !enableControll) return;
        characterAnimation.FlipSprite(directionX);
    }

    public void Move(Vector2 direction)
    {
        if (Time.timeScale == 0f || !enableControll) return;
        characterMovement.HandleMovement(direction.x);
        characterAnimation.FlipSprite(direction.x);
    }

    public void TryJump()
    {
        if (Time.timeScale == 0f || !enableControll) return;
        if (characterMovement.HandleJump())
        {
            characterAnimation.SetJump();
        }
    }

    private IEnumerator HoldAndDestroy(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        Destroy(gameObject);
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