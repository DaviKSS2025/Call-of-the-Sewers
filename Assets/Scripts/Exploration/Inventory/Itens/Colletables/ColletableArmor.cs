using UnityEngine;
using UnityEngine.InputSystem;

public class ColletableArmor : Colletables
{
    [SerializeField] private Armors _armor;
    [SerializeField] private ArmorDatabase _armorDatabase;
    private Armors _playerCurrentArmor;

    public void Start()
    {
        _itemName = _armor.Name;
        _equipmentType = "armor";
    }

    public override void OnPlayerPickup()
    {
        if (_insideRange)
        {
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
}
