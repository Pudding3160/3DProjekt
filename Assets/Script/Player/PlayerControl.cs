using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pause;
    [SerializeField] private UIManager UIManager;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 50f;
    [SerializeField] private float staminaDrain = 10f;
    [SerializeField] private float staminaRegen = 5f;

    private float stamina;

    [Header("Speed")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sneakMul = 0.67f;
    [SerializeField] private float sprintMul = 1.6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityMul = 1f;

    [Header("Look")]
    [SerializeField] private float sens = 0.1f;
    [SerializeField] private float lookRange = 80f;

    [Header("References")]
    [SerializeField] private CharacterController charController;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    [SerializeField] private Interact interact;

    [Header("Shooting")]
    [SerializeField] private Shoot shoot;
    [SerializeField] private int ammoCapacity = 2;

    [Header("Player")]
    public GameObject player;
    public bool die = false;

    private Vector3 currentMovement;
    private float verticalRotation;

    private bool isPaused = false;
    private bool tutorialFinished = false;
    private bool isDying = false;

    private float deathTimer = 0f;

    private float CurrentSpeed
    {
        get
        {
            if (playerInputHandler.IsSneaking)
            {
                return walkSpeed * sneakMul;
            }

            if (playerInputHandler.IsSprinting && stamina > 0f)
            {
                return walkSpeed * sprintMul;
            }

            return walkSpeed;
        }
    }

    private void Start()
    {
        stamina = maxStamina;

        Time.timeScale = 1f;

        SetCursorLocked(true);
    }

    private void Update()
    {
        // Tutorial ending screen is active.
        if (tutorialFinished)
        {
            HandleDeath();
            return;
        }

        // Player is dying.
        if (isDying)
        {
            HandleDeath();
            return;
        }

        // Handle pause input first.
        HandlePause();

        // If paused, reset the one-shot input BEFORE returning.
        if (isPaused)
        {
            playerInputHandler.ResetInputs();
            return;
        }

        // Normal gameplay.
        HandleMovement();
        HandleRotation();
        HandleShooting();
        HandleStamina();
        HandleInteract();
        HandleSound();
        HandleDeath();

        // Reset one-shot inputs.
        playerInputHandler.ResetInputs();
    }

    // =========================================================
    // PAUSE
    // =========================================================

    private void HandlePause()
    {
        if (playerInputHandler.PauseTriggered)
        {
            Debug.Log("Pause Niggered");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (tutorialFinished)
        {
            return;
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;

        SetCursorLocked(false);

        if (pause != null)
        {
            pause.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        SetCursorLocked(true);

        if (pause != null)
        {
            pause.SetActive(false);
        }
    }

    // =========================================================
    // TUTORIAL END
    // =========================================================

    public void CursorUnlock()
    {
        tutorialFinished = true;
        isPaused = false;

        Time.timeScale = 0f;

        SetCursorLocked(false);
    }

    // =========================================================
    // CURSOR
    // =========================================================

    private void SetCursorLocked(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            sens = 0.1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            sens = 0f;
        }
    }

    // =========================================================
    // MOVEMENT
    // =========================================================

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

    private void HandleMovement()
    {
        Vector3 worldDirection = CalcWorldDirection();

        currentMovement.x =
            worldDirection.x * CurrentSpeed;

        currentMovement.z =
            worldDirection.z * CurrentSpeed;

        charController.Move(
            currentMovement * Time.deltaTime
        );
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

    // =========================================================
    // ROTATION
    // =========================================================

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

    // =========================================================
    // STAMINA
    // =========================================================

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
            stamina -= staminaDrain * Time.deltaTime;
            stamina = Mathf.Max(stamina, 0f);
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Min(stamina, maxStamina);
        }
    }

    // =========================================================
    // SHOOTING
    // =========================================================

    private void HandleShooting()
    {
        if (!playerInputHandler.shootTriggered)
        {
            return;
        }

        if (ammoCapacity <= 0)
        {
            return;
        }

        shoot.Shooting();

        ammoCapacity--;

        if (UIManager != null)
        {
            UIManager.UpdateAmmo(
                ammoCapacity.ToString()
            );
        }
    }

    // =========================================================
    // INTERACTION
    // =========================================================

    private void HandleInteract()
    {
        if (playerInputHandler.InteractTriggered)
        {
            interact.interact();
        }
    }

    // =========================================================
    // SOUND
    // =========================================================

    private void HandleSound()
    {
        bool isMoving =
            playerInputHandler.MoveInput.sqrMagnitude > 0.01f;

        bool isSprinting =
            playerInputHandler.IsSprinting &&
            isMoving &&
            stamina > 0f;

        bool isSneaking =
            playerInputHandler.IsSneaking &&
            isMoving;

        if (!isMoving)
        {
            AudioManager.instance.Stop("Walk");
            AudioManager.instance.Stop("Sprint");
            AudioManager.instance.Stop("Sneak");
        }
        else if (isSneaking)
        {
            AudioManager.instance.Stop("Walk");
            AudioManager.instance.Stop("Sprint");
            AudioManager.instance.Play("Sneak");
        }
        else if (isSprinting)
        {
            AudioManager.instance.Stop("Walk");
            AudioManager.instance.Stop("Sneak");
            AudioManager.instance.Play("Sprint");
        }
        else
        {
            AudioManager.instance.Stop("Sprint");
            AudioManager.instance.Stop("Sneak");
            AudioManager.instance.Play("Walk");
        }
    }

    // =========================================================
    // DEATH
    // =========================================================

    private void HandleDeath()
    {
        if (die && !isDying)
        {
            Die();
        }

        if (isDying)
        {
            deathTimer += Time.unscaledDeltaTime;

            if (deathTimer >= 1.5f)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void Die()
    {
        if (isDying)
        {
            return;
        }

        isDying = true;

        walkSpeed = 0f;
        sens = 0f;
        deathTimer = 0f;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("SpiderBite");
        }

        if (player != null)
        {
            Destroy(player, 2f);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}