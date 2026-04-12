using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Attacks/AttackData")]
public class AttackData : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private string _attackName;
    [SerializeField] private int _attackChance;
    [SerializeField] private int _criticalChance;

    [SerializeField] private List<StatusEffectEntry> _statusList;
    public int Damage
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
    public List<StatusEffectEntry> StatusList
    {
        get => _statusList;
    }
}
