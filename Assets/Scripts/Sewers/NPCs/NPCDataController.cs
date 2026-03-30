using System.Collections.Generic;
using UnityEngine;

public class NPCDataController : MonoBehaviour
{
    public static NPCDataController Instance;

    public List<AllyNPC> RuntimeData { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RuntimeData = CloneList(SaveManager.Instance.Data.NPCData);
    }

    private List<AllyNPC> CloneList(List<AllyNPC> original)
    {
        var clone = new List<AllyNPC>();

        foreach (var npc in original)
        {
            clone.Add(new AllyNPC
            {
                CurrentHealth = npc.CurrentHealth,
                NPCInfo = npc.NPCInfo
            });
        }

        return clone;
    }

    public void RecruitNPC(NPCType npcType)
    {
        RuntimeData.Add(new AllyNPC
        {
            NPCInfo = npcType,
            CurrentHealth = 100 // ou valor padrão
        });
    }

    public void ApplyDamage(NPCType type, int value)
    {
        var npc = RuntimeData.Find(n => n.NPCInfo == type);

        if (npc != null)
        {
            npc.CurrentHealth -= value;
        }
    }

    public void Save()
    {
        SaveManager.Instance.Data.NPCData = CloneList(RuntimeData);
        SaveManager.Instance.Save();
    }
}