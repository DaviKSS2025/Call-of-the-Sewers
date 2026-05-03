using UnityEngine;

[CreateAssetMenu(fileName = "DarkHoldTargetSkillData", menuName = "Player/Skills/Dark Hold")]

public class DarkHoldTargetSkillData : TargetAttackSkillData
{
    public override BaseSkillBehaviour CreateInstance(ISkillUser user)
    {
        return new DarkHoldSkillBehaviour(this, user);
    }
}
