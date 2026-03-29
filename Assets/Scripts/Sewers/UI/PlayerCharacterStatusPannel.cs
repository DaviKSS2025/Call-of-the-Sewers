using UnityEngine;
using UnityEngine.UI;
public class PlayerCharacterStatusPannel : BaseCharacterStatusPannel
{
    [SerializeField] protected Sprite _characterSprite;
    [SerializeField] private Slider _sanitySlider;
    public override void UpdateTexts()
    {
        _characterName.text = SaveManager.Instance.Data.PlayerData.PlayerName;
        _weaponName.text = SaveManager.Instance.Data.PlayerData.CurrentWeapon.Name;
        _armorName.text = SaveManager.Instance.Data.PlayerData.CurrentArmor.Name;
        int attackPower = (int)SaveManager.Instance.Data.PlayerData.CurrentWeapon.DamageMultiplier * 100;
        _weaponAttackPower.text = attackPower.ToString();
        int defensePower = (int)SaveManager.Instance.Data.PlayerData.CurrentArmor.DefenseMultiplier * 100;
        _armorDefensePower.text = defensePower.ToString();
    }
    public override void UpdateImages()
    {
        _characterImage.sprite = _characterSprite;
        _weaponImage.sprite = SaveManager.Instance.Data.PlayerData.CurrentWeapon.WeaponSprite;
        _armorImage.sprite = SaveManager.Instance.Data.PlayerData.CurrentArmor.ArmorSprite;
    }
    public override void UpdateSliders()
    {
        _healthSlider.value = SaveManager.Instance.Data.PlayerData.CurrentHealth/100;
        _sanitySlider.value = SaveManager.Instance.Data.PlayerData.CurrentMana/100;
    }
}
