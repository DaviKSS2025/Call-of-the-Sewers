using System;
using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public static MapDataController Instance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    private MapExplorationData _runTimeExplorationData;
    private EnemiesExplorationData _enemyEncounteredInCombat;

    public MapExplorationData RuntimeExplorationData => _runTimeExplorationData;
    public EnemiesExplorationData EnemyEncounteredInCombat => _enemyEncounteredInCombat;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        _runTimeExplorationData = SaveManager.Instance.Data.ExplorationData;
    }
    public Vector2 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _runTimeExplorationData.WorldPosX = player.transform.position.x;
        _runTimeExplorationData.WorldPosY = player.transform.position.y;
        return new Vector2(_runTimeExplorationData.WorldPosX, _runTimeExplorationData.WorldPosY);
    }
    public void UsedSacrificePlace()
    {
        _runTimeExplorationData.UsedSacrificePlace = true;
    }
    public void OpenDoor(string doorName)
    {
        _runTimeExplorationData.OpenedDoors[doorName] = true;
    }
    public void ItemFound(string itemName)
    {
        _runTimeExplorationData.CollectedItems.Add(itemName, true);
    }
    public MapExplorationData GetSaveInfo()
    {
        GetPlayerPosition();
        return _runTimeExplorationData;
    }
    public void EnemyCombatTriggered(string enemyId)
    {
        if (_runTimeExplorationData.EnemyExplorationInfo.TryGetValue(enemyId, out EnemiesExplorationData enemy))
        {
            _enemyEncounteredInCombat = enemy;
        }
        GetPlayerPosition();
    }
    public void UpdateEnemyPositions(EnemiesExplorationData enemiesData)
    {
        if (RuntimeExplorationData.EnemyExplorationInfo.ContainsKey(enemiesData.Id))
        {
            RuntimeExplorationData.EnemyExplorationInfo[enemiesData.Id] = enemiesData;
        }
        else
        {
            RuntimeExplorationData.EnemyExplorationInfo.Add(enemiesData.Id, enemiesData);
        }
    }
    public void EnemyDeathOnCombat()
    {
        if(_runTimeExplorationData.EnemyExplorationInfo.TryGetValue(_enemyEncounteredInCombat.Id, out EnemiesExplorationData enemy))
        {
            enemy.Dead = true;
            _runTimeExplorationData.EnemyExplorationInfo[_enemyEncounteredInCombat.Id] = enemy;
        }
    }
}
