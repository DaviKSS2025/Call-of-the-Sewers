using System.Collections.Generic;
public class DarkHoldSkillBehaviour : BaseTargetAttackSkillBehaviour
{
    private List<StatusEffectEntry> _statusList = new List<StatusEffectEntry>();
    public DarkHoldSkillBehaviour(TargetAttackSkillData data) : base(data)
    {
        _data = data;
    }
    public override void UsingSkill()
    {
        InstanceNewStunEffect();
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.ComChannel.RaiseTargetAttackSkillRequested(_data.Damage, _statusList, _data.CriticalChance);
        base.UsingSkill();
        _controller.SelectionChannel.RaiseSelectionConfirmed();
    }

    private void InstanceNewStunEffect()
    {
        _statusList.Clear();

        StatusEffectEntry baseEntry = _data.StatusList[0];

        StatusEffectEntry newEntry = new StatusEffectEntry(baseEntry.StatusType, baseEntry.StatusChance, IncreaseStunDurationWithLessMana());

        _statusList.Add(newEntry);
    }
    private int IncreaseStunDurationWithLessMana()
    {
        if (_controller.Stats.CurrentMana > _controller.SurvStats.MaxMana * 0.8f)
        {
            return 1;
        }
        else if (_controller.Stats.CurrentMana > _controller.SurvStats.MaxMana * 0.5f)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
