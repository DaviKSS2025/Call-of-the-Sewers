using System.Collections.Generic;
using UnityEngine;

public class NPCDataController : MonoBehaviour
{
    public static NPCDataController Instance;

    public List<AllyNPC> RuntimeData { get; private set; }
    public List<NPCType> NPCHistoric {  get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RuntimeData = CloneList(SaveManager.Instance.Data.NPCData);
        NPCHistoric = SaveManager.Instance.Data.AlreadyRecruitedNPCs;
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
            CurrentHealth = 100
        });
        NPCHistoric.Add(npcType);
    }
    public void RemoveNPC(NPCType npcType)
    {
        for (int i = 0; i < RuntimeData.Count; i++)
        {
            if (RuntimeData[i].NPCInfo == npcType)
            {
                RuntimeData.RemoveAt(i);
                break;
            }
        }
    }
    public void RecoverHealth(int value)
    {
        RuntimeData[0].CurrentHealth = Mathf.Min(100, RuntimeData[0].CurrentHealth + value);
    }
}