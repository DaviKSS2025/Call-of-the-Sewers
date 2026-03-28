using UnityEngine;
public abstract class StatsController
{
    protected int _currentHealth;
    protected int _currentMana;
    protected BaseEntityController _entity;

    public int CurrentHealth
    {
        get => _currentHealth;
    }
    public int CurrentMana
    {
        get => _currentMana;
        set => _currentMana = value;
    }
    public StatsController(BaseEntityController entity)
    {
        _entity = entity;
    }
    public void OnSufferingAttack(AttackData attack, float damageMultiplier)
    {
        TakeDamage(attack, damageMultiplier);
    }
    public virtual void TakeDamage(AttackData attack, float damageMultiplier)
    {
        _currentHealth -= CalculateDamage(attack, damageMultiplier);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _entity.SurvStats.MaxHealth);

        if (_currentHealth > 0)
        {
            _entity.AnimatorStateController.PlayTakingDamage();
        }
        else
        {
            _entity.AnimatorStateController.PlayDeath();
        }
    }
    private int CalculateDamage(AttackData attack, float damageMultiplier)
    {
        int damage = Mathf.RoundToInt(attack.Damage * damageMultiplier * (1 - _entity.SurvStats.Defense));
        
        if (RollCritical(attack.CriticalChance))
        {
            damage *= 2;
        }
        return damage;
    }
    private bool RollCritical(int criticalChance)
    {
        return criticalChance >= Random.Range(1, 101);
    }
    public void UseMana(int manaCost)
    {
        CurrentMana = Mathf.Max(0, CurrentMana - manaCost);
    }
}
