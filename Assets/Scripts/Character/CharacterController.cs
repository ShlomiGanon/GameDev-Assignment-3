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
        characterAnimation = GetComponent<CharacterAnimation>();
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
        characterMovement.HandleMovement(direction.x);
        //characterAnimation.SetMovement(direction);
    }

    public void TryJump()
    {
        if (Time.timeScale == 0f || !enableControll) return;
        if (characterMovement.HandleJump())
        {
            //characterAnimation.PlayJump();
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
        GameEvents.DialogueEnd += EnableController;
        GameEvents.DialogueStart += DisableController; 
    }

    private void OnDisable()
    {
        InputEvents.Move -= Move;
        InputEvents.Jump -= TryJump;
    }
}