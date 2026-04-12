using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Devoured")]
public class DevouredEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target, int duration)
    {
        return new DevouredEffectInstance(this,target, duration);
    }
}
