public class NPCStatsController : StatsController
{
    protected CharacterStatsUI _NPCStatsUI;
    public NPCStatsController(BaseEntityController entity, CharacterStatsUI NPCStatsUI, int currentHealth) : base(entity)
    {
        _NPCStatsUI = NPCStatsUI;
        _currentHealth = currentHealth;

        InitializeNPCUI();
    }
    public void InitializeNPCUI()
    {
        _NPCStatsUI.Initialize(_entity.EntityNameString, this, _entity.SurvStats.MaxHealth);
        _NPCStatsUI.OnHealthChanged();
    }
    public override void TakeDamage(int baseDamage, float damageMultiplier, float criticalChanceMultiplier, int baseCriticalChance)
    {
        base.TakeDamage(baseDamage, damageMultiplier, criticalChanceMultiplier, baseCriticalChance);
        _NPCStatsUI.OnHealthChanged();
    }
    public override void TakeExactDamage(int damage)
    {
        base.TakeExactDamage(damage);
        _NPCStatsUI.OnHealthChanged();
    }
}
