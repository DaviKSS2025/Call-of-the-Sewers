using UnityEngine;
using UnityEngine.InputSystem;

public class ColletableWeapon : MonoBehaviour
{
    [SerializeField] private Weapons _weapon;
    private bool _isideRange;
    private InputSystem_Actions _inputActions;

    private void Start()
    {
        _inputActions = new InputSystem_Actions();

        _inputActions.UI.Submit.performed += OnToggleMenuPerformed;

        _inputActions.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isideRange = true;
        }
    }
}
