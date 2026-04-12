using UnityEngine;

[CreateAssetMenu(fileName = "TargetAttackSkillData", menuName = "Player/TargetAttackSkillData")]
public abstract class TargetAttackSkillData : SkillData
{
    [SerializeField] protected int _damage;
    [SerializeField] protected int _criticalChance;
    public int Damage => _damage;
    public int CriticalChance => _criticalChance;
}
