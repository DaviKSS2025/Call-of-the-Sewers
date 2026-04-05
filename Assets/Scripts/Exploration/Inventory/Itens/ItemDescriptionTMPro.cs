using TMPro;
using UnityEngine;

public class ItemDescriptionTMPro : MonoBehaviour
{
    [SerializeField] private InventoryChannel _inventoryChannel;
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        _inventoryChannel.ItemSelected += UpdateItemDescription;
    }
    private void OnDisable()
    {
        _text.text = "No items selected.";
        _inventoryChannel.ItemSelected -= UpdateItemDescription;
    }
    private void UpdateItemDescription(string itemDescription)
    {
        _text.text = itemDescription;
    }
}
