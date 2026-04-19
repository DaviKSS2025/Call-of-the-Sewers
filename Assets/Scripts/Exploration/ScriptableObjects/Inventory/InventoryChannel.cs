using UnityEngine;
using System;
[CreateAssetMenu(fileName = "InventoryChannel", menuName = "Channels/InventoryChannel")]
public class InventoryChannel : ScriptableObject
{
    public Action<string> ItemSelected;
    public Action<ConsumableItemData> ItemUsedOnTarget;
    public Action OpenSelectTargetOnStatusPannel;
    public Action<TorchEffect> TorchUsed;
    public Action TorchActive;
    public Action MatchesUsed;
    public Action InstantItemUsed;
    public Action TorchEnd;
    public Action<bool> EnteredLightArea;
    public Action<bool> EnteredEnteredMatchesTriggerArea;
    public void RaiseItemSelected(string itemDescription)
    {
        ItemSelected?.Invoke(itemDescription);
    }
    public void RaiseItemUsedOnTarget(ConsumableItemData itemUsed)
    {
        ItemUsedOnTarget?.Invoke(itemUsed);
        OpenSelectTargetOnStatusPannel?.Invoke();
    }
    public void RaiseTorchUsed(TorchEffect torch)
    {
        TorchUsed?.Invoke(torch);
        InstantItemUsed?.Invoke();
        TorchActive?.Invoke();
    }
    public void RaiseMatchesUsed()
    {
        MatchesUsed?.Invoke();
    }
    public void RaiseTorchEnd()
    {
        TorchEnd?.Invoke();
    }
    public void RaiseEnteredLightArea(bool isEntering)
    {
        EnteredLightArea?.Invoke(isEntering);
    }
    public void RaiseEnteredMatchesTriggerArea(bool isEntering)
    {
        EnteredEnteredMatchesTriggerArea?.Invoke(isEntering);
    }
}
