using System.Collections.Generic;
using System;
public class TurnOrderManager : IEntityListHandler
{
    private TurnChangeChannel _turnChangeChannel;
    private List<BaseEntityController> _turnOrder = new();
    public Action OnPlayerVictory;
    public Action OnPlayerDefeated;
    public Action OnEndGame;
    public TurnOrderManager(TurnChangeChannel turnChangeChannel, List<BaseEntityController> turnOrder) 
    { 
        _turnChangeChannel = turnChangeChannel;
        _turnOrder = turnOrder;
    }
    public List<BaseEntityController> TurnOrder
    {
        get => _turnOrder;
    }

    public void Initialize()
    {
        _turnChangeChannel.OnEntityDeath += OnEntityDeath;
        _turnChangeChannel.OnPlayerRan += SaveAlliesStats;
    }
    private void OnEntityDeath(BaseEntityController entity)
    {
        if (entity.EntityType == TargetType.Player)
        {
            OnPlayerDeath(entity);
        }
        else if (entity.EntityType == TargetType.NPC)
        {
            OnNPCDeath(entity);
        }
        else
        {
            OnEnemyDeath(entity);
        }
    }
    private void OnPlayerDeath(BaseEntityController player)
    {
        PlayerDefeated();
    }
    private void OnNPCDeath(BaseEntityController npc)
    {
        _turnOrder.Remove(npc);
        _turnChangeChannel.RaiseTurnOrderChanged(_turnOrder);
    }
    private void OnEnemyDeath(BaseEntityController enemy)
    {
        _turnOrder.Remove(enemy);
        foreach (BaseEntityController firstEnemy in _turnOrder)
        {
            if (firstEnemy.EntityType == TargetType.Enemy)
            {
                _turnChangeChannel.RaiseTurnOrderChanged(_turnOrder);
                return;
            }
        }
        PlayerVictory();
    }
    private void PlayerVictory()
    {
        MapDataController.Instance.EnemyDeathOnCombat();
        SaveAlliesStats();
        OnEndGame?.Invoke();
        OnPlayerVictory?.Invoke();
    }
    private void PlayerDefeated()
    {
        OnEndGame?.Invoke();
        OnPlayerDefeated?.Invoke();
        ResetSaves();
    }
    public void OnDisable()
    {
        _turnChangeChannel.OnEntityDeath -= OnEntityDeath;
        _turnChangeChannel.OnPlayerRan -= SaveAlliesStats;
    }
    private void SaveAlliesStats()
    {
        foreach (BaseEntityController entity in _turnOrder)
        {
            if (entity.EntityType == TargetType.Player)
            {
                PlayerDataController.Instance.SetHealth(entity.Stats.CurrentHealth);
                PlayerDataController.Instance.SetMana(entity.Stats.CurrentMana);
            }
            else if (entity.EntityType == TargetType.NPC) 
            {
                NPCController npcController = entity as NPCController;
                NPCDataController.Instance.SetHealth(entity.Stats.CurrentHealth, npcController.NPCType);
            }
        }
    }
    private void ResetSaves()
    {
        NPCDataController.Instance.CloneSave();
        PlayerDataController.Instance.CloneSave();
        InventoryDataController.Instance.CloneSave();
        MapDataController.Instance.CloneSave();
    }
}
public interface IEntityListHandler
{
    public List<BaseEntityController> TurnOrder
    {
        get;
    }
}