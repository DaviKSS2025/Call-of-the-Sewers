using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private EnemyDatabase _database;

    private int _chanceToSpawnMultiples;

    public List<BaseEntityController> Initialize()
    {
        List<BaseEntityController> enemies = new();

        ManageStartEnemySpawn(enemies);

        return enemies;
    }

    private void ManageStartEnemySpawn(List<BaseEntityController> enemies)
    {
        SpawnEnemy(MapDataController.Instance.EnemyEncounteredInCombat.EnemyType, enemies);

        if (WillSpawnMultipleEnemies(_chanceToSpawnMultiples))
        {
            SpawnEnemy(MapDataController.Instance.EnemyEncounteredInCombat.EnemyType, enemies);
        }
    }

    private void SpawnEnemy(EnemyType type, List<BaseEntityController> enemies)
    {
        GameObject prefab = _database.GetEnemyPrefab(type);

        _chanceToSpawnMultiples = _database.GetMultipleSpawnChance(type);

        GameObject instance = Instantiate(prefab, transform);

        var entity = instance.GetComponent<BaseEntityController>();

        enemies.Add(entity);
    }

    private bool WillSpawnMultipleEnemies(int chance)
    {
        return Random.Range(0, 101) < chance;
    }
}
