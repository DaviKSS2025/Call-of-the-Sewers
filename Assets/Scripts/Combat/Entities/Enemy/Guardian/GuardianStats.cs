using UnityEngine;

public class GuardianStats : StatsController
{
    public GuardianStats(BaseEntityController entity) : base(entity)
    {
        _currentHealth = entity.SurvStats.MaxHealth;
    }
}
