using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private float maxStamina = 50f;
    [SerializeField] private float staminaDrain = 10f;
    [SerializeField] private float staminaRegen = 5f;

    private float stamina;

    [Header("Speed values")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sneakMul = 0.67f;
    [SerializeField] private float sprintMul = 1.5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityMul = 1f;

    [Header("Look")]
    [SerializeField] private float sens = 0.1f;
    [SerializeField] private float lookRange = 80f;

    [Header("Refs")]
    [SerializeField] private CharacterController charController;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Interact interact;

    [Header("Shooting")]
    [SerializeField] private Shoot shoot;
    private float ammoCapacity = 2f;

    private Vector3 currentMovement;
    private float verticalRotation;

    private float CurrentSpeed
    {
        get
        {
            // Sneaking takes priority over sprinting
            if (playerInputHandler.IsSneaking)
                return walkSpeed * sneakMul;

            // Only sprint if the player has stamina
            if (playerInputHandler.IsSprinting && stamina > 0f)
                return walkSpeed * sprintMul;

            return walkSpeed;
        }
    }

    private void Start()
    {
        stamina = maxStamina;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
        HandleStamina();
        HandleInteract();

        // Reset one-shot inputs after they have been processed
        playerInputHandler.ResetInputs();
    }

    private void HandleInteract()
    {
        if (playerInputHandler.InteractTriggered)
        {
            interact.interact();
        }
    }

    private void HandleStamina()
    {
        bool isMoving =
            playerInputHandler.MoveInput.sqrMagnitude > 0.01f;

        bool isActuallySprinting =
            playerInputHandler.IsSprinting &&
            isMoving &&
            stamina > 0f;

        if (isActuallySprinting)
        {
            // Drain stamina
            stamina -= staminaDrain * Time.deltaTime;

            // Prevent stamina from going below zero
            stamina = Mathf.Max(stamina, 0f);
        }
        else
        {
            // Regenerate stamina
            stamina += staminaRegen * Time.deltaTime;

            // Prevent stamina from going above maximum
            stamina = Mathf.Min(stamina, maxStamina);
        }

       //debug.Log("Stamina: " + stamina);
    }

    private void HandleShooting()
    {
        if (playerInputHandler.shootTriggered && ammoCapacity>0)
        {
            shoot.Shooting();
            ammoCapacity -= 1;
        }
    }

    private Vector3 CalcWorldDirection()
    {
        Vector3 inputDirection = new Vector3(
            playerInputHandler.MoveInput.x,
            0f,
            playerInputHandler.MoveInput.y
        );

        Vector3 worldDirection =
            transform.TransformDirection(inputDirection);

        return worldDirection.normalized;
    }

    private void HandleJumping()
    {
        if (charController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (playerInputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y +=
                Physics.gravity.y *
                gravityMul *
                Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalcWorldDirection();

        currentMovement.x =
            worldDirection.x * CurrentSpeed;

        currentMovement.z =
            worldDirection.z * CurrentSpeed;

        HandleJumping();

        charController.Move(
            currentMovement * Time.deltaTime
        );
    }

    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(
            0f,
            rotationAmount,
            0f
        );
    }

    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(
            verticalRotation - rotationAmount,
            -lookRange,
            lookRange
        );

        cam.transform.localRotation =
            Quaternion.Euler(
                verticalRotation,
                0f,
                0f
            );
    }

    private void HandleRotation()
    {
        float mouseXRotation =
            playerInputHandler.RotationInput.x * sens;

        float mouseYRotation =
            playerInputHandler.RotationInput.y * sens;

        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);
    }
}