using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("InputAction")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action map name ref")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action map name ref")]
    [SerializeField] private string movement = "Move";
    [SerializeField] private string rotation = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string crouch = "Crouch";

    private InputAction moveAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction crouchAction;

    public Vector2 MoveInput {  get; private set; }
    public Vector2 RotationInput {  get; private set; }
    public bool JumpTrigger {  get; private set; }
    public bool CrouchTrigger {  get; private set; }

    private void Awake()
    {
        InputActionMap mapRef=playerControls.FindActionMap(actionMapName);

        moveAction=mapRef.FindAction(movement);
        rotationAction=mapRef.FindAction(rotation);
        jumpAction=mapRef.FindAction(jump);
        crouchAction=mapRef.FindAction(crouch);

        SubActionValuestoInput();
    }

    private void SubActionValuestoInput()
    {
        moveAction.performed += inputInfo => MoveInput=  inputInfo.ReadValue<Vector2>();
        moveAction.canceled += inputInfo => MoveInput = Vector2.zero;

        rotationAction.performed+= inputInfo=>RotationInput= inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        jumpAction.performed += inputInfo => JumpTrigger = true;
        jumpAction.canceled += inputInfo => JumpTrigger = false;


        crouchAction.performed += inputInfo => CrouchTrigger = true;
        crouchAction.canceled += inputInfo => CrouchTrigger = false;

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
