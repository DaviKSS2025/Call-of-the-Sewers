using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDatabase", menuName = "Scriptable Objects/NPCDatabase")]
public class NPCDatabase : ScriptableObject
{
    [Serializable]
    public struct NPCEntry
    {
        public NPCType Type;
        public EntityName NPCName;
        public Sprite StatusSprite;
        public Weapons Weapon;
        public Armors Armor;
        public SurvivalStats SurvivalStats;
        public GameObject prefab;
    }

    [SerializeField] private NPCEntry[] NPCS;



    public GameObject GetNPCPrefab(NPCType NPC)
    {
        NPCEntry currentNPC = CheckNPCExistence(NPC);
        return currentNPC.prefab;
    }
    public string GetNPCName(NPCType NPC)
    {
        NPCEntry currentNPC = CheckNPCExistence(NPC);
        return currentNPC.NPCName.Name;
    }
    public Weapons GetNPCWeapon(NPCType NPC)
    {
        NPCEntry currentNPC = CheckNPCExistence(NPC);
        return currentNPC.Weapon;
    }
    public Armors GetNPCArmor(NPCType NPC)
    {
        NPCEntry currentNPC = CheckNPCExistence(NPC);
        return currentNPC.Armor;
    }
    public Sprite GetNPCStatusSprite(NPCType NPC)
    {
        NPCEntry currentNPC = CheckNPCExistence(NPC);
        return currentNPC.StatusSprite;
    }
    private NPCEntry CheckNPCExistence(NPCType NPC)
    {
        foreach (NPCEntry npcFound in NPCS)
        {
            if(npcFound.Type == NPC)
            {
                return npcFound;
            }
        }
        return NPCS[0];
    }
}
public enum NPCType
{
    DJones
}