using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] PlayerState currentState;
    List<PlayerStateListener> listeners = new List<PlayerStateListener>();
    Vector3 lookDirection;

    public Vector3 GetLookDirection()
    {
        return lookDirection;
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    public void SetSprintState()
    {
        ShowWeapon(false);
        ChangeState(PlayerState.IS_SPRINTING);
    }

    public void SetIdleState()
    {
        ShowWeapon(true);
        ChangeState(PlayerState.IS_IDLE);
    }

    public void SetRunningState()
    {
        ShowWeapon(true);
        ChangeState(PlayerState.IS_RUNNING);
    }

    public void SetDancingState()
    {
        ShowWeapon(false);
        ChangeState(PlayerState.IS_DANCING);
    }

    public void SetDyingState()
    {
        ShowWeapon(false);
        ChangeState(PlayerState.IS_DYING);
    }


    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
        foreach (PlayerStateListener listener in listeners)
        {
            listener.onPlayerStateChange(newState);
        }
    }

    private void ShowWeapon(bool isEnabled)
    {
        foreach (PlayerStateListener listener in listeners)
        {
            listener.OnWeaponStatusChange(isEnabled);
        }
    }

    public void AddListener(PlayerStateListener listener)
    {
        listeners.Add(listener);
    }
}
