using UnityEngine;
public class DarkHealingSkillBehaviour : BaseSkillBehaviour
{
    private IDarkHealingSkillUser _healInfo;
    public DarkHealingSkillBehaviour(SkillData data, ISkillUser user, IDarkHealingSkillUser healInfo) : base(data, user)
    {
        _target = "allies";
        _healInfo = healInfo;
    }
    public override void PreparingSkill()
    {
        _stringToShow = $"<color=red>{_user.ControllerName}</color> cast <color=red>{Data.Name}</color> on <color=red>{_target}</color>";
        UsingSkill();
    }
    public override void UsingSkill()
    {
        Debug.Log("Usando Dark Healing");
        _user.UseGlobalHealingSkill(_healInfo.GetHealAmount());
    }
}
