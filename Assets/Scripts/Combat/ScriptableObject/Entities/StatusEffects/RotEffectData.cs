using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Rot")]
public class RotEffectData : StatusEffectData
{
    public float DamagePerTurn;

    public override StatusEffectInstance CreateInstance(BaseEntityController target, int duration)
    {
        return new RotEffectInstance(this, target, duration, DamagePerTurn);
    }
}
