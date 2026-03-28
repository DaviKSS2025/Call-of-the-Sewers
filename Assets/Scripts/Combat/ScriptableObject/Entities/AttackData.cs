using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attacks/AttackData")]
public class AttackData : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private string _attackName;
    [SerializeField] private int _attackChance;
    [SerializeField] private int _criticalChance;
    [SerializeField] private StatusEffectData _statusEffect;

    public float Damage
    {
        get => _damage;
    }
    public string AttackName
    {
        get => _attackName;
    }
    public int AttackChance
    {
        get => _attackChance;
    }
    public int CriticalChance
    {
        get => _criticalChance;
    }
    public StatusEffectData StatusEffect
    {
        get => _statusEffect;
    }
}
