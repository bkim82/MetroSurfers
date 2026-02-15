using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    [Header("Forward")]
    public float forwardSpeed = 10f;

    [Header("Lanes")]
    public float laneOffset = 3f;            // lane x positions: -3, 0, +3
    public float laneChangeSpeed = 12f;      // higher = snappier lane swap
    private int laneIndex = 1;               // 0 left, 1 middle, 2 right

    [Header("Jump/Gravity")]
    public float jumpHeight = 2.2f;
    public float gravity = -25f;
    private float verticalVelocity;

    [Header("Roll/Slide")]
    public float rollDuration = 0.7f;
    public float rollHeight = 1.0f;          // CharacterController height while rolling

    private CharacterController cc;
    private float originalHeight;
    private Vector3 originalCenter;
    private bool isRolling;
    private float rollTimer;

    public Transform visual;
    private Vector3 originalVisualScale;

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

        // Smoothly move toward target lane (position)
        float targetX = (laneIndex - 1) * laneOffset;
        float x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneChangeSpeed);

        // Grounding
        if (cc.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        // Gravity (verticalVelocity is a velocity)
        verticalVelocity += gravity * Time.deltaTime;

        // Move this frame:
        // - X uses delta position (already per-frame)
        // - Y/Z use velocities * dt
        float deltaX = x - transform.position.x;
        Vector3 motion = new Vector3(deltaX, verticalVelocity * Time.deltaTime, forwardSpeed * Time.deltaTime);

        cc.Move(motion);
    }

    void HandleInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // Lane change
        if (kb.aKey.wasPressedThisFrame || kb.leftArrowKey.wasPressedThisFrame)
            laneIndex = Mathf.Max(0, laneIndex - 1);

        if (kb.dKey.wasPressedThisFrame || kb.rightArrowKey.wasPressedThisFrame)
            laneIndex = Mathf.Min(2, laneIndex + 1);

        // Jump
        bool jumpPressed = kb.spaceKey.wasPressedThisFrame || kb.wKey.wasPressedThisFrame || kb.upArrowKey.wasPressedThisFrame;
        if (jumpPressed && cc.isGrounded && !isRolling)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Roll / slide
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
            visual.localScale.x,
            visual.localScale.y / 4f,
            visual.localScale.z
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
}