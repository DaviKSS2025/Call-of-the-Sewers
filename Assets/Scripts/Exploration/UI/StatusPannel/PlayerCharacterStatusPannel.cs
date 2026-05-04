using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCharacterStatusPannel : BaseCharacterStatusPannel
{
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private Slider _sanitySlider;
    [SerializeField] private TextMeshProUGUI _sanityValue;
    [SerializeField] private WeaponDatabase _weaponDatabase;
    [SerializeField] private ArmorDatabase _armorDatabase;
    private Weapons _currentWeapon;
    private Armors _currentArmor;

    public override void UpdateCharacterStatus()
    {
        _currentWeapon = _weaponDatabase.GetWeaponScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentWeapon);
        _currentArmor = _armorDatabase.GetArmorScriptableObject(PlayerDataController.Instance.RuntimeData.CurrentArmor);
        base.UpdateCharacterStatus();
    }
    public override void UpdateTexts()
    {
        _characterName.text = PlayerDataController.Instance.RuntimeData.PlayerName;
        _weaponName.text = _currentWeapon.Name;
        _armorName.text = _currentArmor.Name;
        int attackPower = (int)(_currentWeapon.DamageMultiplier * 100);
        _weaponAttackPower.text = attackPower.ToString();
        int defensePower = (int)(_currentArmor.DefenseMultiplier * 100);
        _armorDefensePower.text = defensePower.ToString();
        _healthValue.text = PlayerDataController.Instance.RuntimeData.CurrentHealth.ToString();
        _sanityValue.text = PlayerDataController.Instance.RuntimeData.CurrentMana.ToString();
    }
    public override void UpdateImages()
    {
        //_characterImage.sprite = _characterSprite;
        _weaponImage.sprite = _currentWeapon.WeaponSprite;
        _armorImage.sprite = _currentArmor.ArmorSprite;
    }
    public override void UpdateSliders()
    {
        _healthSlider.value = PlayerDataController.Instance.RuntimeData.CurrentHealth /100f;
        _sanitySlider.value = PlayerDataController.Instance.RuntimeData.CurrentMana /100f;
    }
}
