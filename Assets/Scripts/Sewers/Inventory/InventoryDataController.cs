using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class InventoryDataController : MonoBehaviour
{
    public static InventoryDataController Instance;

    private List<ConsumableItemData> ItemList;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ItemList = Clone(SaveManager.Instance.Data.Items);
    }
    public void Save()
    {
        SaveManager.Instance.Data.Items = ItemList;
        SaveManager.Instance.Save();
    }
    private List<ConsumableItemData> Clone(List<ConsumableItemData> original)
    {
        return new List<ConsumableItemData>(original);
    }
    public List<ConsumableItemData> GetItemList() 
    { 
        return ItemList;
    }
    public void OnItemUsed(ConsumableItemData itemType)
    {
        ItemList.Remove(itemType);
    }
    public void OnItemAdded(ConsumableItemData itemType)
    {
        ItemList.Add(itemType);
    }
}
