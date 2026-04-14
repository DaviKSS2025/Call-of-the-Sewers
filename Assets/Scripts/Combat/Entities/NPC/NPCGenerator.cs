using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class NPCGenerator : MonoBehaviour
{
    //[SerializeField] private GameObject NPCpannel;
    [SerializeField] private NPCDatabase _database;
    private List<BaseEntityController> _generatedNPCS = new();
    public List<BaseEntityController> Initialize()
    {
        _generatedNPCS.Clear();

        if (isNPCAlive())
        {
            SpawnAllyObjects();
        }

        return _generatedNPCS;
    }
    private void SpawnAllyObjects()
    {
        //NPCpannel.SetActive(true);
        SpawnNPC();
    }
    private void SpawnNPC()
    {
        foreach (AllyNPC npc in SaveManager.Instance.Data.NPCData)
        {
            GameObject prefab = _database.GetNPCPrefab(npc.NPCInfo);

            GameObject instance = Instantiate(prefab, transform);

            _generatedNPCS.Add(instance.GetComponent<BaseEntityController>());
        }
    }
    private bool isNPCAlive()
    {
        return SaveManager.Instance.Data.NPCData != null && SaveManager.Instance.Data.NPCData.Count > 0;
    }
    
}
