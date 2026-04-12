using System.Diagnostics;

public class DarkFireSkillBehaviour : BaseTargetAttackSkillBehaviour
{
    public DarkFireSkillBehaviour(TargetAttackSkillData data) : base(data) 
    { 
        _data = data;
    }
    public override void UsingSkill()
    {
        _controller.ThisInputChannel.OnUICancel -= CancelingUse;
        _controller.ThisInputChannel.OnSubmit -= UsingSkill;
        _controller.ComChannel.RaiseTargetAttackSkillRequested(UpdateDamageBasedOnMana(), _data.StatusList, _data.CriticalChance);
        _controller.Stats.UseMana(Data.ManaCost);
        _controller.AnimatorStateController.PlaySkill();
        _controller.SelectionChannel.RaiseSelectionConfirmed();
    }
    private int UpdateDamageBasedOnMana()
    {
        float bonusMultiplier = (100f - _controller.Stats.CurrentMana) / 100f;
        return (int)(_data.Damage * (1f + (bonusMultiplier * 0.5f)));
    }
}
