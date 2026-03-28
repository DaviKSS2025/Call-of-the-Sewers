using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private EnemyDatabase _database;

    private int _chanceToSpawnMultiples = 15;
    private int _decreaseChanceToMultipleSpawn = 15;

    public List<BaseEntityController> Initialize()
    {
        List<BaseEntityController> enemies = new();

        ManageStartEnemySpawn(enemies);

        return enemies;
    }

    private void ManageStartEnemySpawn(List<BaseEntityController> enemies)
    {
        SpawnEnemy(CombatContext.LastEncounteredEnemy, enemies);

        while (WillSpawnMultipleEnemies())
        {
            _chanceToSpawnMultiples -= _decreaseChanceToMultipleSpawn;

            SpawnEnemy(CombatContext.LastEncounteredEnemy, enemies);
        }
    }

    private void SpawnEnemy(EnemyType type, List<BaseEntityController> enemies)
    {
        GameObject prefab = _database.GetEnemyPrefab(type);

        GameObject instance = Instantiate(prefab, transform);

        var entity = instance.GetComponent<BaseEntityController>();

        enemies.Add(entity);
    }

    private bool WillSpawnMultipleEnemies()
    {
        return Random.Range(0, 101) < _chanceToSpawnMultiples;
    }
}
