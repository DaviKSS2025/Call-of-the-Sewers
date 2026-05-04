public class DarkHealingSkillBehaviour : BaseSkillBehaviour
{
    private int _healAmount;
    private IDarkHealingSkillUser _healInfo;
    public DarkHealingSkillBehaviour(SkillData data, ISkillUser user, IDarkHealingSkillUser healInfo) : base(data, user)
    {
        _target = "allies";
        _healInfo = healInfo;
    }

    public override void UsingSkill()
    {
        _user.UseGlobalHealingSkill(_healInfo.GetHealAmount());
    }
}
