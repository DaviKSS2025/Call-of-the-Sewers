using UnityEngine;
public class ColletableWeapon : Colletables
{
    [SerializeField] private Weapons _weapon;
    [SerializeField] private WeaponDatabase _weaponDatabase;
    private Weapons _playerCurrentWeapon;
    public override void Start()
    {
        base.Start();
        _itemName = _weapon.Name;
        _equipmentType = "weapon";
    }
    public override void OnPlayerPickup()
    {
        if (_insideRange)
        {
            wasCollected = true;
            _playerCurrentWeapon = _weaponDatabase.GetWeaponScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentWeapon);
            _currentEquipmentName = GetCurrentEquipmentName();
            if (_weapon.DamageMultiplier > _playerCurrentWeapon.DamageMultiplier)
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
        PlayerDataController.Instance.UpgradeWeapon(_weapon.ThisWeaponType);
        base.UpgradeEquipment();
    }
    public override string GetCurrentEquipmentName()
    {
        return _playerCurrentWeapon.Name;
    }
    public override bool CheckIfWasAlreadyPicked()
    {
        MapDataController.Instance.RuntimeExplorationData.CollectedItems.TryGetValue(_weapon.Name, out bool wasFind);
        return wasFind;
    }
}
