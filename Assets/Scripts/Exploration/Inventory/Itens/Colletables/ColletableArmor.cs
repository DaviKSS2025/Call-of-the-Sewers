using System.Collections;
using UnityEngine;

public class ColletableArmor : Colletables
{
    [SerializeField] private Armors _armor;
    [SerializeField] private ArmorDatabase _armorDatabase;
    private Armors _playerCurrentArmor;

    public override void Start()
    {
        base.Start();
        _itemName = _armor.Name;
        _equipmentType = "armor";
    }

    public override void OnPlayerPickup()
    {
        if (_insideRange)
        {
            base.OnPlayerPickup();
            wasCollected = true;
            _playerCurrentArmor = _armorDatabase.GetArmorScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentArmor);
            _currentEquipmentName = GetCurrentEquipmentName();
            if (_armor.DefenseMultiplier > _playerCurrentArmor.DefenseMultiplier)
            {
                UpgradeEquipment();
            }
            else
            {
                DontPickWorseEquipment();
            }
            MapDataController.Instance.ItemFound(_armor.Name);
        }
    }
    public override void UpgradeEquipment()
    {
        PlayerDataController.Instance.UpgradeArmor(_armor.ThisArmorType);
        base.UpgradeEquipment();
    }
    public override string GetCurrentEquipmentName()
    {
        return _playerCurrentArmor.Name;
    }
    public override bool CheckIfWasAlreadyPicked()
    {
        MapDataController.Instance.RuntimeExplorationData.CollectedItems.TryGetValue(_armor.Name, out bool wasFind);
        return wasFind;
    }
}
