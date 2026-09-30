/* 2D character movement in bird's eye view x-axis and y-axis
    ! ensure input binding
    ! ensure that gravity is set to 0
    - ensure rigidbody has continous checked off in collision detection
    - dynamic is set so boxcolliders can interact
    ? do you have a boxcollider for 'boundaries'
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private float moveX;
    private float moveY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveX = 0f;
        moveY = 0f;

        // 1. Read Keyboard (WASD or Arrow Keys)
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveX = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveX = 1f;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveY = 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveY = -1f;
        }

        // 2. Read Gamepad (Left Stick with D-Pad fallback)
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 stickInput = gamepad.leftStick.ReadValue();
            Vector2 dpadInput = gamepad.dpad.ReadValue();

            // Use stick if it's pushed past deadzone, otherwise use D-pad
            Vector2 finalGamepadInput = stickInput.magnitude > 0.1f ? stickInput : dpadInput;

            // If either is being used, override the keyboard input
            if (finalGamepadInput.magnitude > 0.1f)
            {
                moveX = finalGamepadInput.x;
                moveY = finalGamepadInput.y;
            }
        } 

        moveInput = new Vector2(moveX, moveY).normalized; 
    } 

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}