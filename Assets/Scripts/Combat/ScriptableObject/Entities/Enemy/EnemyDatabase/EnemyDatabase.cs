using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Databases/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    [Serializable]
    public struct EnemyEntry
    {
        public EnemyType type;
        public int multipleSpawnChance;
        public GameObject prefab;
    }

    [SerializeField] private EnemyEntry[] enemies;

    public GameObject GetEnemyPrefab(EnemyType type)
    {
        foreach (var e in enemies)
        {
            if (e.type == type)
                return e.prefab;
        }
        return null;
    }
    public int GetMultipleSpawnChance(EnemyType type)
    {
        foreach (var e in enemies)
        {
            if (e.type == type)
                return e.multipleSpawnChance;
        }
        return 0;
    }
}
public enum EnemyType
{
    Guardian,
    Cerberus
}