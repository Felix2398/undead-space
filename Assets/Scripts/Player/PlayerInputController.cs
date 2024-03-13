using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerStateController playerStateController;
    private PlayerWeaponController playerWeaponController;
    private PlayerMovementController playerMovementController;

    // Start is called before the first frame update
    void Start()
    {
        playerStateController = GetComponent<PlayerStateController>();
        playerWeaponController = GetComponent<PlayerWeaponController>();
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
}
