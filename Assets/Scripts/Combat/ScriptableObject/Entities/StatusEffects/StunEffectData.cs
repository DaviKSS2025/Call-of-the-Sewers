using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Stun")]
public class StunEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target)
    {
        return new StunEffectInstance(this, target, Duration);
    }
}