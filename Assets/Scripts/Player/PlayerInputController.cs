using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour, PlayerStateListener
{
    private PlayerStateController playerStateController;
    private PlayerWeaponController playerWeaponController;
    private PlayerMovementController playerMovementController;
    private bool isDead = false;

    private void Awake() 
    {
        playerStateController = GetComponent<PlayerStateController>();
        playerWeaponController = GetComponent<PlayerWeaponController>();
        playerMovementController = GetComponent<PlayerMovementController>();
        playerStateController.AddListener(this);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isMoving = h != 0 || v != 0;

        if (Input.GetButton("Fire1"))
        {
            playerWeaponController.FireCurrentWeapon();
        }

        if (isMoving && Input.GetKey(KeyCode.LeftShift)) 
        {
            playerStateController.SetSprintState();
        } 
        else if (isMoving)
        {
            playerStateController.SetRunningState();
        }
        else if (playerStateController.GetCurrentState() != PlayerState.IS_DANCING)
        {
            playerStateController.SetIdleState();
        }
        
        playerMovementController.Move(h, v);
    }

    private void Update() 
    {
        if (isDead) return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.E))
        {
            playerWeaponController.EquipPreviousWeapon();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.Q))
        {
            playerWeaponController.EquipNextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerStateController.SetDancingState();
        }
    }

    public void onPlayerStateChange(PlayerState newState) 
    {
        if (newState == PlayerState.IS_DYING)
        {
            isDead = true;
        }
    }
}
