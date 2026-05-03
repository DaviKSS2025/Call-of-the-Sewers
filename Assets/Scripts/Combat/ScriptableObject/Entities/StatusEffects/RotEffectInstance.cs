using UnityEngine;

public class RotEffectInstance : StatusEffectInstance
{
    private float _maxHealthDamagePercentage;
    public RotEffectInstance(StatusEffectData data,BaseEntityController target, int duration, float damagePercentage): base(data, target, duration)
    {
        _maxHealthDamagePercentage = damagePercentage;
    }
    public override void OnTurn()
    {
        _target.Stats.TakeExactDamage(Mathf.RoundToInt(_target.SurvStats.MaxHealth/ _maxHealthDamagePercentage));
    }
}
