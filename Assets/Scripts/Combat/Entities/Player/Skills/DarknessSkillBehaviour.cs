public class DarknessSkillBehaviour : BaseSkillBehaviour
{
    private StatusEffectData _statusEffect;
    private int _statusChance;
    private int _duration;
    public DarknessSkillBehaviour(SkillData data, ISkillUser user, StatusEffectData statusEffect, int statusEffectChance, int duration) : base(data, user)
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
        _user.UseGlobalStatusEffectSkill(_statusEffect, _statusChance, _stringToShow, _duration);
    }
}
