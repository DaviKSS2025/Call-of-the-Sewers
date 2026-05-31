using UnityEngine;

public class CerberusStats : StatsController
{
    public CerberusStats(BaseEntityController entity) : base(entity)
    {
        _currentHealth = entity.SurvStats.MaxHealth;
    }
}
