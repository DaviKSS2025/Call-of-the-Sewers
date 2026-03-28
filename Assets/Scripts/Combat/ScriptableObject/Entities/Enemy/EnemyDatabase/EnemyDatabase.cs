using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Scriptable Objects/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    [Serializable]
    public struct EnemyEntry
    {
        public EnemyType type;
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

        Debug.LogError($"Enemy {type} não encontrado");
        return null;
    }
}
public enum EnemyType
{
    Guardian,
    Cerberus
}