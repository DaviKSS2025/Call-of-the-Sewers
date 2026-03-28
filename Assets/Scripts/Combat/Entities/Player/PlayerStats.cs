using UnityEngine;

public class PlayerStats : StatsController
{
    private CharacterStatsUI _playerStatsUI;

    public PlayerStats(BaseEntityController entity, CharacterStatsUI playerStatsUI) : base(entity)
    {
        _currentHealth = SaveManager.Instance.Data.PlayerData.CurrentHealth;
        _currentMana = SaveManager.Instance.Data.PlayerData.CurrentMana;
        _playerStatsUI = playerStatsUI;
        _playerStatsUI.Initialize(entity.EntityNameString, this, entity.SurvStats.MaxHealth, _entity.SurvStats.MaxMana);
    }

    public override void TakeDamage(AttackData attack, float damageMultiplier)
    {
        base.TakeDamage(attack, damageMultiplier);
        _playerStatsUI.OnHealthChanged();
    }
}
