
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private VirtualJoystick virtualJoystick;

    [Header("Arena Boundaries")]
    [SerializeField]
    private Vector2 arenaMin =
        new Vector2(-99f, -99f);

    [SerializeField]
    private Vector2 arenaMax =
        new Vector2(99f, 99f);

    [SerializeField] private float boundaryPadding = 0.5f;

    private Rigidbody2D rb;
    private PlayerStats stats;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;

        rb.interpolation =
            RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        Vector2 keyboardInput = GetKeyboardInput();
        Vector2 joystickInput = GetJoystickInput();

        if (joystickInput.sqrMagnitude > 0.01f)
        {
            movement = joystickInput;
        }
        else
        {
            movement = keyboardInput;
        }

        movement = Vector2.ClampMagnitude(movement, 1f);
    }

    private Vector2 GetKeyboardInput()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current == null)
            return input;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            input.y += 1f;
        }

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
        {
            input.y -= 1f;
        }

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            input.x -= 1f;
        }

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            input.x += 1f;
        }

        return input.normalized;
    }

    private Vector2 GetJoystickInput()
    {
        if (virtualJoystick == null)
            return Vector2.zero;

        return virtualJoystick.InputDirection;
    }

    private void FixedUpdate()
    {
        if (stats == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 velocity =
            movement * stats.MoveSpeed;

        Vector2 position = rb.position;

        float minX =
            arenaMin.x + boundaryPadding;

        float maxX =
            arenaMax.x - boundaryPadding;

        float minY =
            arenaMin.y + boundaryPadding;

        float maxY =
            arenaMax.y - boundaryPadding;

        // Stop movement towards a boundary before
        // the Rigidbody moves outside the arena.
        if (position.x <= minX && velocity.x < 0f)
        {
            velocity.x = 0f;
        }
        else if (position.x >= maxX && velocity.x > 0f)
        {
            velocity.x = 0f;
        }

        if (position.y <= minY && velocity.y < 0f)
        {
            velocity.y = 0f;
        }
        else if (position.y >= maxY && velocity.y > 0f)
        {
            velocity.y = 0f;
        }

        rb.linearVelocity = velocity;
    }
}
