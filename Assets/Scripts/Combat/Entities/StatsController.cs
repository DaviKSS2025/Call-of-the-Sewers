using System.Collections.Generic;
using UnityEngine;
public abstract class StatsController
{
    protected int _currentHealth;
    protected BaseEntityController _entity;
    protected int _currentMana;

    public int CurrentHealth
    {
        get => _currentHealth;
    }
    public int CurrentMana
    {
        get => _currentMana;
    }
    public StatsController(BaseEntityController entity)
    {
        _entity = entity;
    }
    public void OnSufferingAttack(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, List<StatusEffectEntry> statusList, int baseCriticalChance)
    {
        TakeDamage(baseDamage, damageMultiplier, criticalChanceMultiplier, baseCriticalChance);
        ManageStatusEffect(statusList);
    }
    public virtual void TakeDamage(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, int baseCriticalChance)
    {
        _currentHealth -= CalculateDamage(baseDamage, damageMultiplier, criticalChanceMultiplier, baseCriticalChance);
        ResolveAfterDamage();
    }
    public virtual void TakeExactDamage(int damage)
    {
        _currentHealth -= damage;
        ResolveAfterDamage();
    }
    private void ResolveAfterDamage()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _entity.SurvStats.MaxHealth);
        if (_currentHealth > 0)
        {
            _entity.AnimatorStateController.PlayTakingDamage();
        }
        else
        {
            _entity.DeathClears();
            _entity.AnimatorStateController.PlayDeath();
            _entity.ThisTurnChangeChannel.OnEntityDeath(_entity);
            _entity.ComChannel.RaiseEntityKilled(_entity.EntityNameString);
        }
    }
    private void ManageStatusEffect(List<StatusEffectEntry> statusList)
    {
        if (statusList.Count > 0)
        {
            foreach (StatusEffectEntry status in statusList)
            {
                _entity.StatusManager.ApplyEffect(status.StatusType, status.StatusChance, status.Duration);
            }
        }
    }
    private int CalculateDamage(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, int baseCriticalChance)
    {
        int damage = Mathf.RoundToInt(baseDamage * damageMultiplier * (1 - _entity.SurvStats.Defense));
        
        if (RollCritical(baseCriticalChance, criticalChanceMultiplier))
        {
            damage *= 2;
        }
        return damage;
    }
    private bool RollCritical(int criticalChance, float criticalChanceMultiplier)
    {
        return criticalChance*criticalChanceMultiplier >= Random.Range(1, 101);
    }
    public virtual void UseMana(int manaCost)
    {
        _currentMana = Mathf.Max(0, _currentMana - manaCost);
    }
}
