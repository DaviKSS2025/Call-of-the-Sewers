using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Stun")]
public class StunEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target, int duration)
    {
        return new StunEffectInstance(this, target, duration);
    }
}