using UnityEngine;
using System;
[CreateAssetMenu(fileName = "InventoryChannel", menuName = "Channels/InventoryChannel")]
public class InventoryChannel : ScriptableObject
{
    public Action<string> ItemSelected;
    public Action<ConsumableItemData> ItemUsedOnTarget;
    public Action OpenSelectTargetOnStatusPannel;
    public Action TorchUsed;
    public Action MatchesUsed;
    public void RaiseItemSelected(string itemDescription)
    {
        ItemSelected?.Invoke(itemDescription);
    }
    public void RaiseItemUsedOnTarget(ConsumableItemData itemUsed)
    {
        ItemUsedOnTarget?.Invoke(itemUsed);
        OpenSelectTargetOnStatusPannel?.Invoke();
    }
    public void RaiseTorchUsed()
    {
        TorchUsed?.Invoke();
    }
    public void RaiseMatchesUsed()
    {
        MatchesUsed?.Invoke();
    }
}
