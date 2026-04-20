using UnityEngine;

public class NeurostimulantsEffect : ItemEffect
{
    [SerializeField] private int _manaHealFactor;
    public override void OnUsed()
    {
        base.OnUsed();
        _inventoryChannel.RaiseInstantItemUsed();
        PlayerDataController.Instance.RecoverMana(_manaHealFactor);
    }
}
