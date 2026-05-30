using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class InventoryDataController : MonoBehaviour
{
    public static InventoryDataController Instance;

    private List<ItemType> InventoryList;
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

        CloneSave();
    }
    private List<ItemType> Clone(List<ItemType> original)
    {
        return new List<ItemType>(original);
    }
    public List<ItemType> GetItemList() 
    { 
        return InventoryList;
    }
    public List<string> GetKeyIDs()
    {
        return KeyIDS;
    }
    public void OnItemUsed(ItemType itemType)
    {
        InventoryList.Remove(itemType);
    }
    public void OnItemAdded(ItemType itemType)
    {
        InventoryList.Add(itemType);
    }
    public void AddKey(string keyName)
    {
        KeyIDS.Add(keyName);
    }
    public void CloneSave()
    {
        InventoryList = Clone(SaveManager.Instance.Data.Items);
        KeyIDS = SaveManager.Instance.Data.KeyIds;
    }
}
