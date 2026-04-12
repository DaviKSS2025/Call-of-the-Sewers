using UnityEngine;

[CreateAssetMenu(fileName = "Darkness", menuName = "Player/Skills/Darkness")]
public class DarknessSkillData : SkillData
{
    public override BaseSkillBehaviour CreateInstance()
    {
        return new DarknessSkillBehaviour(this, StatusList[0].StatusType, StatusList[0].StatusChance, StatusList[0].Duration);
    }
    public override int GetManaCost(BaseEntityController controller)
    {
        float manaRatio = (float)controller.Stats.CurrentMana / controller.SurvStats.MaxMana;

        float reduction = Mathf.Lerp(0.75f, 0f, manaRatio);
        float finalCost = ManaCost * (1 - reduction);

        return Mathf.Max(1, Mathf.RoundToInt(finalCost));
    }
}
