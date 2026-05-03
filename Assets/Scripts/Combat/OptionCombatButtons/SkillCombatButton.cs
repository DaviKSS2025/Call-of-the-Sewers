using UnityEngine;

public class SkillCombatButton : OptionCombatBaseButton
{
    [SerializeField] private SkillInventoryController _skillInventoryController;
    public override void OnUsed()
    {
        base.OnUsed();
        _skillInventoryController.StartSelectingSkill();
    }
}
