
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Names")]
    [SerializeField] private string movement = "Move";
    [SerializeField] private string rotation = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sneak = "Crouch";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string shoot = "Attack";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string pause = "Pause";


    private InputAction moveAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction sneakAction;
    private InputAction sprintAction;
    private InputAction shootAction;
    private InputAction interactAction;
    private InputAction pauseAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 RotationInput { get; private set; }

    public bool JumpTriggered { get; private set; }

    public bool IsSneaking { get; private set; }
    public bool IsSprinting { get; private set; }

    public bool shootTriggered { get; private set; }

    public bool InteractTriggered { get; private set; }

    public bool PauseTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap mapRef =
            playerControls.FindActionMap(actionMapName);

        moveAction = mapRef.FindAction(movement);
        rotationAction = mapRef.FindAction(rotation);
        jumpAction = mapRef.FindAction(jump);
        sneakAction = mapRef.FindAction(sneak);
        sprintAction = mapRef.FindAction(sprint);
        shootAction = mapRef.FindAction(shoot);
        interactAction= mapRef.FindAction(interact);  
        pauseAction= mapRef.FindAction(pause);

        SubscribeToInput();
    }

    private void SubscribeToInput()
    {
        
        // Movement
        moveAction.performed += inputInfo =>
            MoveInput = inputInfo.ReadValue<Vector2>();

        moveAction.canceled += inputInfo =>
            MoveInput = Vector2.zero;

        // Rotation
        rotationAction.performed += inputInfo =>
            RotationInput = inputInfo.ReadValue<Vector2>();

        rotationAction.canceled += inputInfo =>
            RotationInput = Vector2.zero;

        // Jump
        jumpAction.performed += inputInfo =>
            JumpTriggered = true;

        // Sneak / Crouch
        sneakAction.performed += inputInfo =>
            IsSneaking = true;

        sneakAction.canceled += inputInfo =>
            IsSneaking = false;

        // Sprint
        sprintAction.performed += inputInfo =>
            IsSprinting = true;

        sprintAction.canceled += inputInfo =>
            IsSprinting = false;

        //shoot
        shootAction.performed += inputInfo => shootTriggered = true;

        interactAction.performed += inputInfo =>
    InteractTriggered = true;

        //pause
        pauseAction.performed += inputInfo =>
    PauseTriggered = !PauseTriggered;


    }

    public void ResetInputs()
    {
        JumpTriggered = false;
        shootTriggered = false;
        InteractTriggered = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}