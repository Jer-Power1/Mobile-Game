using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private VirtualJoystick virtualJoystick;

    private Rigidbody2D rb;
    private PlayerStats stats;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Vector2 keyboardInput =
            GetKeyboardInput();

        Vector2 joystickInput =
            GetJoystickInput();

        // Keyboard is useful while testing in the Editor.
        // Joystick is the main Android control.
        if (joystickInput.sqrMagnitude > 0.01f)
        {
            movement = joystickInput;
        }
        else
        {
            movement = keyboardInput;
        }

        movement = Vector2.ClampMagnitude(
            movement,
            1f
        );
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
            return;

        rb.linearVelocity =
            movement * stats.MoveSpeed;
    }
}