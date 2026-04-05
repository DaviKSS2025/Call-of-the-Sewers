using UnityEngine;

public class MedicineEffect : ItemEffect
{
    public override void OnUsed()
    {
        _inventoryChannel.RaiseItemUsedOnTarget(_itemData);
    }
}
