using UnityEngine;

[CreateAssetMenu(menuName = "StatusEffects/Devoured")]
public class DevouredEffectData : StatusEffectData
{
    public override StatusEffectInstance CreateInstance(BaseEntityController target)
    {
        return new DevouredEffectInstance(this,target, Duration);
    }
}
