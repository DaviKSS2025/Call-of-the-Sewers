using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackOptionButton : OptionCombatBaseButton
{
    private bool _selecting;
    [SerializeField] protected SelectionChannel _selectionChannel;
    public override void OnUsed()
    {
        base.OnUsed();
        if (!_selecting)
        {
            _selectionChannel.RaiseSelectionStarted(TargetType.Enemy);
            _selecting = true;

            ChangeVerticalButtonNavigation(Navigation.Mode.None);
        }
        else
        {
            _playerController.AttackController.StartAttackAnimation(_playerController.AttackList[0]);
            _selectionChannel.RaiseSelectionConfirmed();
        }
    }

    public override void OnEnable()
    {
        _selectionChannel.SelectionEnd += ResetButton;
        base.OnEnable();
    }

    public override void OnDisable()
    {
        _selectionChannel.SelectionEnd -= ResetButton;
        base.OnDisable();
    }

    public override void ResetButton()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        _selecting = false;

        ChangeVerticalButtonNavigation(Navigation.Mode.Automatic);
    }
}
