using System.Collections.Generic;
using UnityEngine;

public class InventoryListUIManager : MonoBehaviour
{
    [SerializeField] private InventoryDataController _inventoryController;
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<GameObject> _generatedItems = new();

    [SerializeField] private ConsumableItemData _testItem;
    private void OnEnable()
    {
        InventoryDataController.Instance.OnItemAdded(_testItem);
        GenerateItemPrefabs();
    }

    private void OnDisable()
    {
        foreach (GameObject generatedItem in _generatedItems)
        {
            Destroy(generatedItem);
        }
        _generatedItems.Clear();
    }
    private void GenerateItemPrefabs()
    {
        var items = _inventoryController.GetItemList();

        if (items == null || items.Count == 0)
            return;

        foreach (ConsumableItemData item in items)
        {
            var prefab = _itemDatabase.GetItemPrefab(item);

            if (prefab == null)
                continue;

            GameObject instance = Instantiate(prefab, transform);
            _generatedItems.Add(instance);
        }
    }
}