using UnityEngine;

public class CombatInputController : MonoBehaviour
{
    [SerializeField] private InputChannel _inputChannel;

    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();

        _inputActions.UI.RightPressed.performed += _ => _inputChannel.RaiseUIRight();
        _inputActions.UI.LeftPressed.performed += _ => _inputChannel.RaiseUILeft();
        _inputActions.UI.Cancel.performed += _ => _inputChannel.RaiseUICancel();
        _inputActions.UI.Submit.performed += _ => _inputChannel.RaiseSubmit();

        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.UI.RightPressed.performed -= _ => _inputChannel.RaiseUIRight();
        _inputActions.UI.LeftPressed.performed -= _ => _inputChannel.RaiseUILeft();
        _inputActions.UI.Cancel.performed -= _ => _inputChannel.RaiseUICancel();
        _inputActions.UI.Submit.performed -= _ => _inputChannel.RaiseSubmit();

        _inputActions.Disable();
    }
}
