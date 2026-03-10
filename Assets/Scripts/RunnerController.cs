using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float horizontalSpeed = 8f;

    [Header("Jump/Gravity")]
    public float jumpHeight = 2.2f;
    public float gravity = -25f;
    private float verticalVelocity;

    [Header("Roll/Slide")]
    public float rollDuration = 0.7f;
    public float rollHeight = 1.0f;

    [Header("Visual")]
    public Transform visual;

    [Header("Hit Reaction")]
    public float hitSlowSpeed = 3f;
    public float hitRecoverTime = 2f;
    public float hitCooldown = 1f;

    private CharacterController cc;
    private float originalHeight;
    private Vector3 originalCenter;
    private Vector3 originalVisualScale;

    private bool isRolling;
    private float rollTimer;

    private bool isHit;
    private float hitTimer;

    private bool canBeHit = true;
    private float hitCooldownTimer;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        originalVisualScale = visual.localScale;
        originalHeight = cc.height;
        originalCenter = cc.center;
    }

    void Update()
    {
        HandleInput();
        UpdateRoll();
        UpdateHitState();
        UpdateHitCooldown();

        var kb = Keyboard.current;
        if (kb == null) return;

        float horizontalInput = 0f;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)
            horizontalInput = -1f;
        else if (kb.dKey.isPressed || kb.rightArrowKey.isPressed)
            horizontalInput = 1f;

        if (cc.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        float currentForwardSpeed = isHit ? hitSlowSpeed : forwardSpeed;
        float zMove = currentForwardSpeed * Time.deltaTime;

        Vector3 motion = new Vector3(
            horizontalInput * horizontalSpeed * Time.deltaTime,
            verticalVelocity * Time.deltaTime,
            zMove
        );

        cc.Move(motion);
    }

    void HandleInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        bool jumpPressed = kb.spaceKey.wasPressedThisFrame || kb.wKey.wasPressedThisFrame || kb.upArrowKey.wasPressedThisFrame;
        if (jumpPressed && cc.isGrounded && !isRolling)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        bool rollPressed = kb.sKey.wasPressedThisFrame || kb.downArrowKey.wasPressedThisFrame;
        if (rollPressed && !isRolling)
        {
            StartRoll();
        }
    }

    void StartRoll()
    {
        isRolling = true;
        rollTimer = rollDuration;

        cc.height = rollHeight;
        cc.center = new Vector3(originalCenter.x, rollHeight / 2f, originalCenter.z);

        visual.localScale = new Vector3(
            originalVisualScale.x,
            originalVisualScale.y / 4f,
            originalVisualScale.z
        );
    }

    void UpdateRoll()
    {
        if (!isRolling)
            return;

        rollTimer -= Time.deltaTime;

        if (rollTimer <= 0f)
        {
            isRolling = false;

            cc.height = originalHeight;
            cc.center = originalCenter;
            visual.localScale = originalVisualScale;
        }
    }

    void UpdateHitState()
    {
        if (!isHit)
            return;

        hitTimer -= Time.deltaTime;

        if (hitTimer <= 0f)
        {
            isHit = false;
        }
    }

    void UpdateHitCooldown()
    {
        if (canBeHit)
            return;

        hitCooldownTimer -= Time.deltaTime;

        if (hitCooldownTimer <= 0f)
        {
            canBeHit = true;
        }
    }

    public void OnHitObstacle()
    {
        if (!canBeHit)
            return;

        canBeHit = false;
        hitCooldownTimer = hitCooldown;

        isHit = true;
        hitTimer = hitRecoverTime;
    }
}