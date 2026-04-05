using UnityEngine;

public class PlayerStats : StatsController
{
    private CharacterStatsUI _playerStatsUI;
    public PlayerStats(BaseEntityController entity, CharacterStatsUI playerStatsUI) : base(entity)
    {
        _currentHealth = PlayerDataController.Instance.RuntimeData.CurrentHealth;
        _currentMana = PlayerDataController.Instance.RuntimeData.CurrentMana;
        _playerStatsUI = playerStatsUI;
        _playerStatsUI.Initialize(entity.EntityNameString, this, entity.SurvStats.MaxHealth, _entity.SurvStats.MaxMana);
    }

    public override void TakeDamage(AttackData attack, float damageMultiplier, float criticalChanceMultiplier)
    {
        base.TakeDamage(attack, damageMultiplier, criticalChanceMultiplier);
        _playerStatsUI.OnHealthChanged();
    }
    public override void TakeExactDamage(int damage)
    {
        base.TakeExactDamage(damage);
        _playerStatsUI.OnHealthChanged();
    }
}
