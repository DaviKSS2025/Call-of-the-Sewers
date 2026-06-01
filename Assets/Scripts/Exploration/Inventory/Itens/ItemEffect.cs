using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class ItemEffect : MonoBehaviour, IConsumableItem
{
    [SerializeField] protected ConsumableItemData _itemData;
    [SerializeField] protected InventoryChannel _inventoryChannel;
    [SerializeField] protected TextMeshProUGUI _itemNameTMPro;
    [SerializeField] protected Image _itemSprite;
    [SerializeField] protected SFXEventChannel _sfxChannel;
    [SerializeField] protected SimpleSFXEvent _selectSFX;
    [SerializeField] protected SimpleSFXEvent _useSFX;

    public virtual void OnUsed()
    {
        InventoryDataController.Instance.OnItemUsed(_itemData.Type);
        _sfxChannel.RaiseEvent(_useSFX);
    }
    public virtual void OnSelected()
    {
        _inventoryChannel.RaiseItemSelected(_itemData.Description);
        _sfxChannel.RaiseEvent(_selectSFX);
    }
    void OnEnable()
    {
        _itemSprite.sprite = _itemData.SpriteImage;
        _itemNameTMPro.text = _itemData.Name;
    }
}
