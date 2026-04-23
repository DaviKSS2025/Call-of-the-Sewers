using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class InventoryDataController : MonoBehaviour
{
    public static InventoryDataController Instance;

    private List<ConsumableItemData> ItemList;
    private List<string> KeyIDS;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ItemList = Clone(SaveManager.Instance.Data.Items);
        KeyIDS = SaveManager.Instance.Data.KeyIds;
    }
    private List<ConsumableItemData> Clone(List<ConsumableItemData> original)
    {
        return new List<ConsumableItemData>(original);
    }
    public List<ConsumableItemData> GetItemList() 
    { 
        return ItemList;
    }
    public List<string> GetKeyIDs()
    {
        return KeyIDS;
    }
    public void OnItemUsed(ConsumableItemData itemType)
    {
        ItemList.Remove(itemType);
    }
    public void OnItemAdded(ConsumableItemData itemType)
    {
        ItemList.Add(itemType);
    }
    public void AddKey(string keyName)
    {
        KeyIDS.Add(keyName);
    }
}
