using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [Serializable]
    public struct ItemEntry
    {
        public ItemType type;
        public GameObject prefab;
    }
    
    [SerializeField] private ItemEntry[] itens;
    
    public GameObject GetItemPrefab(ItemType type)
    {
        foreach (var e in itens)
        {
            if (e.type == type)
                return e.prefab;
        }
        return null;
    }
}
public enum ItemType
{
    Medicine,
    Neurostimulants,
    Torch
}
