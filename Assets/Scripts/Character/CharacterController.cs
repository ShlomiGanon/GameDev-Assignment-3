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

    private void OnEnable()
    {
        TargetManager.RegisterTarget(gameObject);
    }

    private void OnDisable()
    {
        TargetManager.UnregisterTarget(gameObject);      
    }


    private void Update()
    {
        if (characterAnimation == null)
            return;
        characterAnimation.SetVerticalVelocity(characterMovement.GetVerticalVelocity());
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
        characterAnimation.SetMovement(direction);
    }

    public void TryJump()
    {
        if (Time.timeScale == 0f || !enableControll) return;
        if (characterMovement.HandleJump())
        {
            characterAnimation.PlayJump();
        }
    }

    public void Attack()
    {
        if (Time.timeScale == 0f || !enableControll) return;
        characterAnimation.PlayAttack();
    }

    private IEnumerator HoldAndDestroy(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        Destroy(gameObject);
    }

    public void Die()
    {
        DisableController();
        characterAnimation.PlayDeath();
    }

    public void TakeHit()
    {
        characterAnimation.PlayHit();
    }
}