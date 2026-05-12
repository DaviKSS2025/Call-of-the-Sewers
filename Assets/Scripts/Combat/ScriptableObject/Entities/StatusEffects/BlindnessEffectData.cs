using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Blindness")]
public class BlindnessEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target, int duration)
    {
        return new BlindnessEffectInstance(this,target, duration);
    }
}
