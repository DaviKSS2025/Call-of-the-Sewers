using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class OptionCombatBaseButton : MonoBehaviour, IOptionCombatButton
{
    protected Button _button;
    [SerializeField] protected SFXEventChannel _audioChannel;
    [SerializeField] protected SimpleSFXEvent _selectSound;
    [SerializeField] protected SimpleSFXEvent _useSound;
    [SerializeField] protected TurnChangeChannel _turnChangeChannel;
    [SerializeField] protected PlayerController _playerController;
    [SerializeField] protected CombatChannel _combatChannel;
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
    public virtual void OnEnable()
    {
        SubscribeEvents();
    }
    public virtual void OnDisable()
    {
        UnsubscribeEvents();
    }
    public virtual void SubscribeEvents()
    {
        _turnChangeChannel.OnPlayerTurnStarted += ResetButton;
        _combatChannel.ChoosingSkill += StartedSkillChoose;
        _combatChannel.CancelChoosingSkill += ResetButton;
    }
    public virtual void UnsubscribeEvents()
    {
        _turnChangeChannel.OnPlayerTurnStarted -= ResetButton;
        _combatChannel.ChoosingSkill -= StartedSkillChoose;
        _combatChannel.CancelChoosingSkill -= ResetButton;
    }
    public virtual void StartedSkillChoose()
    {
        _button.interactable = false;
    }
    public virtual void ResetButton()
    {
        _button.interactable = true;
        ChangeVerticalButtonNavigation(Navigation.Mode.Automatic);
    }
    public virtual void ChangeVerticalButtonNavigation(Navigation.Mode mode)
    {
        var nav = _button.navigation;
        nav.mode = mode;
        _button.navigation = nav;
    }
}
