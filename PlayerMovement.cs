/* 
Basic 2D template controller for any gamecontroller and keyboard
    ! Do not forget to bind controller buttons
    * Player moves from left to right only
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    private Rigidbody2D rb;
    private float moveX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveX = 0f;

        // 1. Read Keyboard (A/D or Left/Right Arrow)
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveX = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveX = 1f;
        }

        // 2. Read PS5 Controller (Left Stick or D-Pad)
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            float gamepadX = gamepad.leftStick.x.ReadValue();
            if (Mathf.Abs(gamepadX) < 0.1f)
            {
                gamepadX = gamepad.dpad.x.ReadValue();
            }

            // If gamepad is being pushed, let it override keyboard
            if (Mathf.Abs(gamepadX) > 0.1f)
            {
                moveX = gamepadX;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }
}