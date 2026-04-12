using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private SkillData _skillData;
    private PlayerController _playerController;
    [SerializeField] private CombatChannel _combatChannel;
    [SerializeField] private TextMeshProUGUI _text;
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
        _combatChannel.RaiseSkillUsed(_skillData);
        enabled = false;
    }
    private void OnEnable()
    {
        if (_initialized)
        {
            _text.text = $"{_skillData.Name} (<color=blue>{_skillData.GetManaCost(_playerController)}</color>)";
        }
    }
}
