using UnityEngine;
using UnityEngine.InputSystem;

//Class only to detect player inputs and pass values.

public class PlayerInputReader
{
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private bool wasLeftlastDirection;
    public float Horizontal => moveInput.x;
    public float Vertical => moveInput.y;

    public bool WasLeftlastDirection => wasLeftlastDirection;

    public void Initialize()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Enable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 newInput = context.ReadValue<Vector2>();

        Vector2 delta = newInput - moveInput;

        if (delta.x != 0)
        {
            wasLeftlastDirection = delta.x < 0;
        }

        moveInput = newInput;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
    }
}

