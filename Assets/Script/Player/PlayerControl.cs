using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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

    private Vector3 currentMovement;
    private float verticalRotation;

    private float CurrentSpeed
    {
        get
        {
            // Sneaking takes priority over sprinting
            if (playerInputHandler.IsSneaking)
                return walkSpeed * sneakMul;

            if (playerInputHandler.IsSprinting)
                return walkSpeed * sprintMul;

            return walkSpeed;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();

        // Reset one-shot inputs after they have been processed
        playerInputHandler.ResetInputs();
    }

    private Vector3 CalcWorldDirection()
    {
        Vector3 inputDirection = new Vector3(
            playerInputHandler.MoveInput.x,
            0f,
            playerInputHandler.MoveInput.y
        );

        Vector3 worldDirection = transform.TransformDirection(inputDirection);

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
            currentMovement.y += Physics.gravity.y * gravityMul * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalcWorldDirection();

        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;

        HandleJumping();

        charController.Move(currentMovement * Time.deltaTime);
    }

    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0f, rotationAmount, 0f);
    }

    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(
            verticalRotation - rotationAmount,
            -lookRange,
            lookRange
        );

        cam.transform.localRotation = Quaternion.Euler(
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