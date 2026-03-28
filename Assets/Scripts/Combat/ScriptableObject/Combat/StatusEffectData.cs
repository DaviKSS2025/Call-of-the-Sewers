using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectData", menuName = "Attacks/StatusEffect")]

public class StatusEffectData : ScriptableObject
{
    [SerializeField] private StatusEffect _statusEffectType;
    [SerializeField] private int _statusEffectChance;
    [SerializeField] private int _statusEffectTurnDuration;

    public StatusEffect StatusEffectType
    {
        get => _statusEffectType;
    }
    public int StatusEffectChance
    {
        get => _statusEffectChance;
    }
    public int StatusEffectTurnDuration
    {
        get => _statusEffectTurnDuration;
    }
}
public enum StatusEffect
{
    None,
    Poison,
    Burn,
    Blindness
}
