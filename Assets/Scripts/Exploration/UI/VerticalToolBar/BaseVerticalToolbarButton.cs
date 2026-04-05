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
    [SerializeField] private InputController _inputController;
    private void Awake()
    {
        _button = GetComponent<Button>();
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
