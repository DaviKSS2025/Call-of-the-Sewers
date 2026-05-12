using UnityEngine;

public class BlindnessEffectInstance : StatusEffectInstance
{
    public BlindnessEffectInstance(StatusEffectData data, BaseEntityController target, int duration) : base(data, target, duration) { }

    public override void OnApply()
    {
        _target.AttackController.CriticalChanceMultiplier = 0f;
    }
    public override void OnEnd()
    {
        _target.AttackController.CriticalChanceMultiplier = 1f;
    }
}
