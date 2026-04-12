using UnityEngine;

[CreateAssetMenu(fileName = "DarkFireTargetSkillData", menuName = "Player/Skills/DarkFire")]
public class DarkFireTargetSkillData : TargetAttackSkillData
{
    public override BaseSkillBehaviour CreateInstance()
    {
        return new DarkFireSkillBehaviour(this);
    }
}
