using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private InputSystem_Actions inputActions;

    private void Start()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Menu.Toggle.performed += OnToggleMenuPerformed;

        inputActions.Enable();
    }

    private void OnToggleMenuPerformed(InputAction.CallbackContext context)
    {
        _menu.SetActive(!_menu.activeInHierarchy);
    }
}
