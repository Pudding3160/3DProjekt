using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Speed values")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float crouchMul = 0.6f;
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
    private float CurrentSpeed => walkSpeed * (playerInputHandler.CrouchTrigger ? crouchMul : 1);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private Vector3 CalcWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MoveInput.x, 0f, playerInputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void HandleJumping()
    {
        if (charController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (playerInputHandler.JumpTrigger)
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
        currentMovement.z=worldDirection.z * CurrentSpeed;

        HandleJumping();
        charController.Move(currentMovement*Time.deltaTime);    
    }
    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }
    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation=Mathf.Clamp(verticalRotation-rotationAmount, -lookRange, lookRange);
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    private void HandleRotation()
    {
        float mouseXrotation = playerInputHandler.RotationInput.x * sens;
        float mouseYrotation = playerInputHandler.RotationInput.y * sens;

        ApplyHorizontalRotation(mouseXrotation);
        ApplyVerticalRotation(mouseYrotation);
    }
}
