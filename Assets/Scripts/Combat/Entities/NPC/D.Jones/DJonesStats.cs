public class DJonesStats : StatsController
{
    private CharacterStatsUI _NPCStatsUI;
    public DJonesStats(BaseEntityController entity, CharacterStatsUI NPCStatsUI) : base(entity)
    {
        _currentHealth = PlayerDataController.Instance.RuntimeData.CurrentHealth;
        _currentMana = PlayerDataController.Instance.RuntimeData.CurrentMana;
        _NPCStatsUI = NPCStatsUI;

        foreach (AllyNPC npc in NPCDataController.Instance.RuntimeData)
        {
            if (npc.NPCInfo == _type)
            {
                _statusUI.OnHealthChanged(npc.CurrentHealth, _survivalStats.MaxHealth);
                break;
            }
        }


        _NPCStatsUI.Initialize(entity.EntityNameString, this, entity.SurvStats.MaxHealth);
    }
}
