using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DarkHealingSkillData", menuName = "Player/Skills/Dark Healing")]
public class DarkHealingSkillData : SkillData, IDarkHealingSkillUser
{
    [SerializeField] private int _healAmount;
    [SerializeField] private int _criticalChance;

    public override BaseSkillBehaviour CreateInstance(ISkillUser user)
    {
        return new DarkHealingSkillBehaviour(this, user, this);
    }
    public int GetHealAmount()
    {
        if (RollCritical())
        {
            int criticalHeal = Mathf.RoundToInt(_healAmount * 2f);
            return criticalHeal;
        }
        return _healAmount;
    }

    private bool RollCritical()
    {
        int roll = UnityEngine.Random.Range(0, 100);
        return roll < _criticalChance;
    }
}
public interface IDarkHealingSkillUser
{
    int GetHealAmount();
}
