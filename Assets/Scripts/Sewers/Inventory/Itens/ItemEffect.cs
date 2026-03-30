using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class ItemEffect : MonoBehaviour, IConsumableItem
{
    [SerializeField] protected ConsumableItemData _itemData;
    [SerializeField] protected InventoryChannel _inventoryChannel;
    [SerializeField] protected TextMeshProUGUI _itemNameTMPro;
    [SerializeField] protected Image _itemSprite;
    public virtual void OnUsed()
    {

    }
    public virtual void OnSelected()
    {
        _inventoryChannel.RaiseItemSelected(_itemData.Description);
    }
    void OnEnable()
    {
        _itemSprite.sprite = _itemData.SpriteImage;
        _itemNameTMPro.text = _itemData.Name;
    }
}
