using UnityEngine;

public abstract class BaseTargetAttackSkillBehaviour : BaseSkillBehaviour
{
    protected TargetAttackSkillData _data;
    public BaseTargetAttackSkillBehaviour(TargetAttackSkillData data) : base(data) 
    {
        _data = data;
    }
    public override void PreparingSkill()
    {
        _controller.SelectionChannel.RaiseSelectionStarted(TargetType.Enemy);
        _controller.ThisInputChannel.OnUICancel += CancelingUse;
        _controller.ThisInputChannel.OnSubmit += UsingSkill;
    }
    public override void UsingSkill()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.ComChannel.RaiseTargetAttackSkillRequested(_data.Damage, _data.StatusList, _data.CriticalChance);
        base.UsingSkill();
        _controller.SelectionChannel.RaiseSelectionConfirmed();
    }
    public override void OnSkillEnd()
    {
        base.OnSkillEnd();
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.NeutralTurnEnd();
    }
    public override void CancelingUse()
    {
        base.CancelingUse();
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.ComChannel.RaiseCancelChoosingSkill();
    }
}
