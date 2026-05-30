using UnityEngine;
public class PlayerStats : StatsController
{
    private PlayerStatsUI _playerStatsUI;
    private float _percentualManaLostOnCritical = 0.1f;
    private int _NPCDeathPenalty = 10;
    private float _defense;
    public PlayerStats(BaseEntityController entity, PlayerStatsUI playerStatsUI, float defense) : base(entity)
    {
        _currentHealth = PlayerDataController.Instance.RuntimeData.CurrentHealth;
        _currentMana = PlayerDataController.Instance.RuntimeData.CurrentMana;
        _playerStatsUI = playerStatsUI;
        _playerStatsUI.Initialize(entity.EntityNameString, this, entity.SurvStats.MaxHealth);
        _playerStatsUI.OnHealthChanged();
        _playerStatsUI.MaxMana = entity.SurvStats.MaxMana;
        _defense = defense;
    }

    public override void SubscribeEvents()
    {
        _entity.ThisTurnChangeChannel.OnEntityDeath += NPCDeathPenalty;
        _entity.UnscribeEventsOnDisable += OnDisable;
    }
    private void OnDisable()
    {
        _entity.ThisTurnChangeChannel.OnEntityDeath -= NPCDeathPenalty;
        _entity.UnscribeEventsOnDisable -= OnDisable;
    }
    public override void TakeDamage(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, int baseCriticalChance)
    {
        base.TakeDamage(baseDamage, damageMultiplier, criticalChanceMultiplier, baseCriticalChance);
        _playerStatsUI.OnHealthChanged();
    }
    public override void TakeExactDamage(int damage)
    {
        base.TakeExactDamage(damage);
        _playerStatsUI.OnHealthChanged();
    }
    public override void UseMana(int manaCost)
    {
        base.UseMana(manaCost);
        _playerStatsUI.OnManaChanged();
    }
    public override int CalculateDamage(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, int baseCriticalChance)
    {
        int damage = Mathf.RoundToInt(baseDamage * damageMultiplier * (1 - _defense));

        if (RollCritical(baseCriticalChance, criticalChanceMultiplier))
        {
            damage *= 2;
            UseMana(Mathf.RoundToInt(damage * _percentualManaLostOnCritical));
        }
        return damage;
    }
    private void NPCDeathPenalty(BaseEntityController controller)
    {
        if (controller.EntityType == TargetType.NPC)
        {
            UseMana(_NPCDeathPenalty);
        }
    }
    public override void RestoreHealth(int healthAmount)
    {
        _currentHealth = Mathf.Min(_entity.SurvStats.MaxHealth, _currentHealth + healthAmount);
        _playerStatsUI.OnHealthChanged();
    }
}
