using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDatabase", menuName = "Scriptable Objects/NPCDatabase")]
public class NPCDatabase : ScriptableObject
{
    [Serializable]
    public struct NPCEntry
    {
        public EntityName npcName;
        public GameObject prefab;
    }

    [SerializeField] private NPCEntry[] NPCS;

    public GameObject GetNPCPrefab(EntityName NPC)
    {
        foreach (var e in NPCS)
        {
            if (e.npcName == NPC)
                return e.prefab;
        }

        Debug.LogError($"NPC {NPC} não encontrado");
        return null;
    }
}
