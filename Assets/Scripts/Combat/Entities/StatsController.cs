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
    public void OnSufferingAttack(AttackData attack, float damageMultiplier, float criticalChanceMultiplier)
    {
        TakeDamage(attack, damageMultiplier, criticalChanceMultiplier);
    }
    public virtual void TakeDamage(AttackData attack, float damageMultiplier, float criticalChanceMultiplier)
    {
        _currentHealth -= CalculateDamage(attack, damageMultiplier, criticalChanceMultiplier);
        ManageStatusEffect(attack);
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
            _entity.AnimatorStateController.PlayDeath();
            _entity.ThisTurnChangeChannel.OnEntityDeath(_entity);
            _entity.ComChannel.RaiseEntityKilled(_entity.EntityNameString);
        }
    }
    private void ManageStatusEffect(AttackData attack)
    {
        if (attack.StatusList.Count > 0)
        {
            foreach (AttackData.StatusEffectEntry status in attack.StatusList)
            {
                if (RollStatusEffectChance(status.StatusChance))
                {
                    _entity.StatusManager.ApplyEffect(status.StatusType);
                }
            }
        }
    }
    private bool RollStatusEffectChance(int statusChance)
    {
        return statusChance >= Random.Range(1, 101);
    }
    private int CalculateDamage(AttackData attack, float damageMultiplier, float criticalChanceMultiplier)
    {
        int damage = Mathf.RoundToInt(attack.Damage * damageMultiplier * (1 - _entity.SurvStats.Defense));
        
        if (RollCritical(attack.CriticalChance, criticalChanceMultiplier))
        {
            damage *= 2;
        }
        return damage;
    }
    private bool RollCritical(int criticalChance, float criticalChanceMultiplier)
    {
        return criticalChance*criticalChanceMultiplier >= Random.Range(1, 101);
    }
    public void UseMana(int manaCost)
    {
        _currentMana = Mathf.Max(0, _currentMana - manaCost);
    }
}
