using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Blindness")]
public class BlindnessEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target)
    {
        return new BlindnessEffectInstance(this,target, Duration);
    }
}
