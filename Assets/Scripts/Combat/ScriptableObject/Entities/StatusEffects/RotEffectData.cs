using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Rot")]
public class RotEffectData : StatusEffectData
{
    public float DamagePerTurn;

    public override StatusEffectInstance CreateInstance(BaseEntityController target)
    {
        return new RotEffectInstance(this, target, Duration, DamagePerTurn);
    }
}
