using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, PlayerStateListener
{
    [SerializeField] private LayerMask mouseRayCastHitLayer;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;

    public float RunningSpeed
    {
        get { return runningSpeed; }
        set { runningSpeed = value; }
    }

    public float SprintSpeed
    {
        get { return sprintSpeed; }
        set { sprintSpeed = value; }
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    private float horizontal;
    private float vertical;

    [SerializeField] private bool lookAtMouse;

    public bool LookAtMouse
    {
        get { return lookAtMouse; }
        set { lookAtMouse = value; }
    }

    CharacterController characterController;

    private void Awake() 
    {
        GetComponent<PlayerStateController>().AddListener(this);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (lookAtMouse) HandleLookAtMouse();
        else HandleLookAtMoveDirection();

    }

    public void Move(float horizontal, float vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);
        characterController.Move(moveDirection * Time.deltaTime * currentSpeed);

        Vector3 newPosition = transform.position;
        newPosition.y = 0f;
        transform.position = newPosition;
    }

    public void onPlayerStateChange(PlayerState newState)
    {
        currentSpeed = newState switch
        {
            PlayerState.IS_RUNNING => runningSpeed,
            PlayerState.IS_SPRINTING => sprintSpeed,
            _ => 0
        };

        lookAtMouse = newState switch
        {
            PlayerState.IS_SPRINTING => false,
            _ => true
        };
    }

    private void HandleLookAtMouse()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, mouseRayCastHitLayer))    
        {
            Vector3 hitPointHorizontal = new Vector3(_hit.point.x, transform.position.y, _hit.point.z);
            transform.LookAt(hitPointHorizontal);
        }
    }

    private void HandleLookAtMoveDirection()
    {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 lookDirection = moveDirection + transform.position;
        transform.LookAt(lookDirection);
    }
}
