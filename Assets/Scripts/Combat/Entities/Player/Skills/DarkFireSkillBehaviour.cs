public class DarkFireSkillBehaviour : BaseTargetAttackSkillBehaviour
{
    public DarkFireSkillBehaviour(TargetAttackSkillData data, ISkillUser user) : base(data, user)
    { 
        _targetData = data;
    }
    public override int GetDamage()
    {
        float bonusMultiplier = (100f - _user.CurrentMana) / 100f;
        return (int)(_targetData.Damage * (1f + (bonusMultiplier * 0.5f)));
    }
}
