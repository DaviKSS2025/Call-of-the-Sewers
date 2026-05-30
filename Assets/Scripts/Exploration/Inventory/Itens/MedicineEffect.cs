using UnityEngine;

public class MedicineEffect : ItemEffect, IConsumableEffectOnTarget
{
    [SerializeField] private int _healFactor;
    public override void OnUsed()
    {
        _inventoryChannel.RaiseItemUsedOnTarget(this);
    }
    public void Execute(TargetType type)
    {
        if (type == TargetType.Player)
        {
            PlayerDataController.Instance.RecoverHealth(_healFactor);
        }
        else
        {
            NPCDataController.Instance.RecoverHealth(_healFactor);
        }
        InventoryDataController.Instance.OnItemUsed(_itemData.Type);
    }
}
public interface IConsumableEffectOnTarget
{
    void Execute(TargetType type);
}
