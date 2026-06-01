using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private SkillData _skillData;
    private PlayerController _playerController;
    [SerializeField] private CombatChannel _combatChannel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SimpleSFXEvent _hoverSFX;
    [SerializeField] private SimpleSFXEvent _clickSFX;
    [SerializeField] private SFXEventChannel _SFXChannel;
    private bool _initialized;
    public SkillData ThisSkillData
    {
        get => _skillData;
    }
    public void Initialize(SkillData skill, PlayerController controller)
    {
        _playerController = controller;
        _skillData = skill;
        _text.text = $"{skill.Name} (<color=blue>{skill.GetManaCost(_playerController)}</color>)";
        _initialized = true;
    }
    public void SkillUsed()
    {
        _SFXChannel.RaiseEvent(_clickSFX);
        _combatChannel.RaiseSkillUsed(_skillData);
        enabled = false;
    }
    public void Selected()
    {
        _SFXChannel.RaiseEvent(_hoverSFX);
    }
    private void OnEnable()
    {
        if (_initialized)
        {
            _text.text = $"{_skillData.Name} (<color=blue>{_skillData.GetManaCost(_playerController)}</color>)";
        }
    }
}
