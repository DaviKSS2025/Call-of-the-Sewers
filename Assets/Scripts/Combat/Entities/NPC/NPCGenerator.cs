using UnityEngine;
using System.Collections.Generic;
public class NPCGenerator : MonoBehaviour
{
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
    private void SpawnNPC()
    {
        foreach (AllyNPC npc in NPCDataController.Instance.RuntimeData)
        {
            GameObject prefab = _database.GetNPCPrefab(npc.NPCInfo);

            GameObject instance = Instantiate(prefab, transform);

            _generatedNPCS.Add(instance.GetComponent<BaseEntityController>());
        }
    }
    private bool isNPCAlive()
    {
        return NPCDataController.Instance.RuntimeData != null && NPCDataController.Instance.RuntimeData.Count > 0;
    }
    
}
