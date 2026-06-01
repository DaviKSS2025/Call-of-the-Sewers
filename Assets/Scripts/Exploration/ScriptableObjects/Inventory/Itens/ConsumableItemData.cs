using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItemData", menuName = "Itens/Consumables")]
public class ConsumableItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private ItemType _type;
    public string Name
    {
        get => _name;
    }
    public string Description
    {
        get => _description;
    }
    public Sprite SpriteImage
    {
        get => _sprite;
    }
    public ItemType Type
    {
        get => _type;
    }
}
