using UnityEngine;
public class DarknessSkillBehaviour : BaseSkillBehaviour
{
    private StatusEffectData _statusEffect;
    private int _statusChance;
    private int _duration;
    public DarknessSkillBehaviour(SkillData data, StatusEffectData statusEffect, int statusEffectChance, int duration) : base(data) 
    { 
        _statusEffect = statusEffect;
        _statusChance = statusEffectChance;
        _duration = duration;
    }
    public override void PreparingSkill()
    {
        _target = "all enemies";
        base.PreparingSkill();
        UsingSkill();
    }
    public override void UsingSkill()
    {
        _controller.Stats.UseMana(Data.GetManaCost(_controller));
        _controller.AnimatorStateController.PlaySkill();
        _controller.ComChannel.RaiseGlobalStatusEffectUsed(TargetType.Enemy, _statusEffect, _statusChance, _stringToShow, _duration);
    }
    public override void OnSkillEnd()
    {
        _controller.NeutralTurnEnd();
    }
}
