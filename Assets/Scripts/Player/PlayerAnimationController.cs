using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour, PlayerStateListener
{
    [SerializeField] private Animator animator;

    private void Awake() 
    {
        PlayerStateController playerStateController = GetComponent<PlayerStateController>();
        playerStateController.AddListener(this);
    }

    private void SetAllFalse()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isSprinting", false);
        animator.SetBool("isDancing", false);
        animator.SetBool("isDying", false);
    }

    public void onPlayerStateChange(PlayerState newState)
    {
        SetAllFalse();
        switch(newState)
        {
            case PlayerState.IS_IDLE:
                animator.SetBool("isIdle", true);
                break;
            case PlayerState.IS_RUNNING:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.IS_SPRINTING:
                animator.SetBool("isSprinting", true);
                break;
            case PlayerState.IS_DANCING:
                animator.SetBool("isDancing", true);
                break;
            case PlayerState.IS_DYING:
                animator.SetBool("isDancing", true);
                break;

        }
    }
}
