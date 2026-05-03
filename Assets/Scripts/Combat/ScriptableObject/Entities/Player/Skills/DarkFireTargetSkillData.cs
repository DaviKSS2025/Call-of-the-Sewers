using UnityEngine;

[CreateAssetMenu(fileName = "DarkFireTargetSkillData", menuName = "Player/Skills/DarkFire")]
public class DarkFireTargetSkillData : TargetAttackSkillData
{
    public override BaseSkillBehaviour CreateInstance(ISkillUser user)
    {
        return new DarkFireSkillBehaviour(this, user);
    }
}
