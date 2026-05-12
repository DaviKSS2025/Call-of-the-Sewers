
using System.Collections.Generic;
public abstract class BaseTargetAttackSkillBehaviour : BaseSkillBehaviour
{
    protected TargetAttackSkillData _targetData;

    public TargetAttackSkillData TargetData
    {
        get => _targetData;
    }
    public BaseTargetAttackSkillBehaviour(TargetAttackSkillData data, ISkillUser user) : base(data, user)
    {
        _targetData = data;
    }
    public override void PreparingSkill()
    {
        _stringToShow = $"<color=red>{_user.ControllerName}</color> cast <color=red>{Data.Name}</color> on <color=red>{_target}</color>";
        _user.PrepareTargetSkill();
    }
    public override void UsingSkill()
    {
        _user.UsingTargetSkill();
    }
    public virtual int GetDamage()
    {
        return _targetData.Damage;
    }
    public virtual List<StatusEffectEntry> GetStatusList()
    {
        return _data.StatusList;
    }
}