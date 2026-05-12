using UnityEngine;

public class InputController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    [SerializeField] private InputChannel _inputChannel;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();

        _inputActions.Player.Move.performed += ctx => _inputChannel.RaiseMove(ctx.ReadValue<Vector2>());

        _inputActions.Player.Move.canceled += ctx => _inputChannel.RaiseMove(Vector2.zero);

        _inputActions.Player.Interact.performed += _ => _inputChannel.RaiseInteract();

        _inputActions.UI.Submit.performed += _ => _inputChannel.RaiseSubmit();

        _inputActions.Menu.Toggle.performed += _ => _inputChannel.RaiseMenuToggle();

        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= ctx => _inputChannel.RaiseMove(ctx.ReadValue<Vector2>());

        _inputActions.Player.Move.canceled -= ctx => _inputChannel.RaiseMove(Vector2.zero);

        _inputActions.Player.Interact.performed -= _ => _inputChannel.RaiseInteract();

        _inputActions.UI.Submit.performed -= _ => _inputChannel.RaiseSubmit();

        _inputActions.Menu.Toggle.performed -= _ => _inputChannel.RaiseMenuToggle();

        _inputActions.Disable();
    }
}
