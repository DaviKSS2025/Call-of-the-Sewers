public abstract class BaseTargetAttackSkillBehaviour : BaseSkillBehaviour
{
    protected TargetAttackSkillData _data;
    public BaseTargetAttackSkillBehaviour(TargetAttackSkillData data) : base(data) 
    {
        _data = data;
    }
    public override void PreparingSkill()
    {
        if (_controller.EntityType == TargetType.NPC)
        {
            _controller.ComChannel.RaiseRandomTargetAttackSkillRequested(_controller.EntityType, _data.Damage, _data.StatusList, _data.CriticalChance);
            _controller.AnimatorStateController.PlaySkill();
            return;
        }

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
        if (_controller.EntityType != TargetType.NPC)
        {
            base.OnSkillEnd();
            _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        }
        _controller.NeutralTurnEnd();
    }
    public override void CancelingUse()
    {
        base.CancelingUse();
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.ComChannel.RaiseCancelChoosingSkill();
    }
}