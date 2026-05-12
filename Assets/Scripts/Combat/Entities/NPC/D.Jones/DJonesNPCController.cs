using UnityEngine;

public class DJonesNPCController : NPCController
{
    [SerializeField] private SkillData _darkHold;
    [SerializeField] private SkillData _darkFire;
    [SerializeField] private SkillData _darkHealing;
    [SerializeField] private SkillData _darkness;
 
    protected override void SetupSkillManager()
    {
        AssignSkillManager(new DJonesSkillManager(this));
    }
    protected override void SetupStrategy()
    {
        AssignStrategy(new DJonesStrategy(_animatorStateController, _combatChannel, _darkHold, _darkFire, _darkHealing, _darkness, _entityListHandler));
    }
}
