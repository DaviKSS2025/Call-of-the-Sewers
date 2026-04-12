using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Player/SkillData")]
public abstract class SkillData : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _manaCost;

    [SerializeField] protected List<StatusEffectEntry> _statusList;

    public string Name => _name;
    public int ManaCost => _manaCost;
    public List<StatusEffectEntry> StatusList
    {
        get => _statusList;
    }

    public virtual int GetManaCost(BaseEntityController controller)
    {
        return ManaCost;
    }
    public abstract BaseSkillBehaviour CreateInstance();
}

[Serializable]
public struct StatusEffectEntry
{
    [SerializeField] private StatusEffectData _statusEffect;
    [SerializeField] private int _statusEffectChance;
    [SerializeField] private int _duration;
    public StatusEffectEntry(StatusEffectData effect, int chance, int duration)
    {
        _statusEffect = effect;
        _statusEffectChance = chance;
        _duration = duration;
    }
    public int StatusChance
    {
        get => _statusEffectChance;
    }
    public StatusEffectData StatusType
    {
        get => _statusEffect;
    }
    public int Duration
    {
        get => _duration;
    }
}