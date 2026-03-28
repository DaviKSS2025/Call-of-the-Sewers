using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private GameObject NPCpannel;
    [SerializeField] private NPCDatabase _database;
    private List<BaseEntityController> _generatedNPCS = new();
    public List<BaseEntityController> Initialize()
    {
        _generatedNPCS.Clear();

        if (isNPCAlive())
        {
            SpawnNPC();
        }

        return _generatedNPCS;
    }
    private void SpawnAllyObjects()
    {
        NPCpannel.SetActive(true);
        SpawnNPC();
    }
    private void SpawnNPC()
    {
        GameObject prefab = _database.GetNPCPrefab(SaveManager.Instance.Data.NPCData.NPCname);

        GameObject instance = Instantiate(prefab, transform);

        _generatedNPCS.Add(instance.GetComponent<BaseEntityController>());
    }
    private bool isNPCAlive()
    {
        return SaveManager.Instance.Data.NPCData.NPCname != null;
    }
    
}
