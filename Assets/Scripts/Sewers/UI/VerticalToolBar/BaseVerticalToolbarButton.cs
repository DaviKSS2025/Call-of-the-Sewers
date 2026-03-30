using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class BaseVerticalToolbarButton : MonoBehaviour
{
    protected Button _button;
    [SerializeField] protected SFXEventChannel _audioChannel;
    [SerializeField] protected SimpleSFXEvent _selectSound;
    [SerializeField] protected SimpleSFXEvent _useSound;
    [SerializeField] protected MenuController _menuController;
    protected InputSystem_Actions _inputActions;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _inputActions = new InputSystem_Actions();
    }
    public virtual void OnSelected()
    {
        _audioChannel.RaiseEvent(_selectSound);
    }

    public virtual void OnUsed()
    {
        _audioChannel.RaiseEvent(_useSound);
    }
}
