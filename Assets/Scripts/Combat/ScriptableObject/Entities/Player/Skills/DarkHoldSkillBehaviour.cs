using System.Collections.Generic;
public class DarkHoldSkillBehaviour : BaseTargetAttackSkillBehaviour
{
    private List<StatusEffectEntry> _statusList = new List<StatusEffectEntry>();
    public DarkHoldSkillBehaviour(TargetAttackSkillData data, ISkillUser user) : base(data, user)
    {
        _targetData = data;
    }
    private void InstanceNewStunEffect()
    {
        _statusList.Clear();
        _statusList.Add(new StatusEffectEntry(_data.StatusList[0].StatusType, _data.StatusList[0].StatusChance, IncreaseStunDurationWithLessMana()));
    }
    private int IncreaseStunDurationWithLessMana()
    {

        if (_user.CurrentMana > _user.MaxMana * 0.8f)
        {
            return 1;
        }
        else if (_user.CurrentMana > _user.MaxMana * 0.5f)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
    public override List<StatusEffectEntry> GetStatusList()
    {
        InstanceNewStunEffect();
        return _statusList;
    }
}
