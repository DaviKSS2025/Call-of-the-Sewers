public class PlayerStats : StatsController
{
    private PlayerStatsUI _playerStatsUI;
    public PlayerStats(BaseEntityController entity, PlayerStatsUI playerStatsUI) : base(entity)
    {
        _currentHealth = PlayerDataController.Instance.RuntimeData.CurrentHealth;
        _currentMana = PlayerDataController.Instance.RuntimeData.CurrentMana;
        _playerStatsUI = playerStatsUI;
        _playerStatsUI.Initialize(entity.EntityNameString, this, entity.SurvStats.MaxHealth);
        _playerStatsUI.MaxMana = entity.SurvStats.MaxMana;
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
}
