using UnityEngine;
public class DJonesSkillManager : SkillManager
{
    public DJonesSkillManager(BaseEntityController controller) : base(controller) { }

    public override void PreparingSkill(SkillData skillData)
    {
        _currentSkill = skillData.CreateInstance(this);
        _currentSkill.PreparingSkill();
    }
    public override void PrepareTargetSkill()
    {
        UsingTargetSkill();
    }
    public override void CancelingUse() 
    { 

    }
    public override void UsingTargetSkill()
    {
        BaseTargetAttackSkillBehaviour targetSkill = _currentSkill as BaseTargetAttackSkillBehaviour;

        _controller.ComChannel.RaiseRandomTargetAttackSkillRequested(_controller.EntityType,targetSkill.TargetData.Damage, targetSkill.TargetData.StatusList, targetSkill.TargetData.CriticalChance);
        UseManaAndPlayAnimation();
    }
    protected override void UseManaAndPlayAnimation()
    {
        _controller.AnimatorStateController.PlaySkill();
    }
}
