using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCharacterStatusPannel : BaseCharacterStatusPannel
{
    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private Slider _sanitySlider;
    [SerializeField] protected TextMeshProUGUI _sanityValue;

    public override void UpdateTexts()
    {
        _characterName.text = PlayerDataController.Instance.RuntimeData.PlayerName;
        _weaponName.text = PlayerDataController.Instance.RuntimeData.CurrentWeapon.Name;
        _armorName.text = PlayerDataController.Instance.RuntimeData.CurrentArmor.Name;
        int attackPower = (int)PlayerDataController.Instance.RuntimeData.CurrentWeapon.DamageMultiplier * 100;
        _weaponAttackPower.text = attackPower.ToString();
        int defensePower = (int)PlayerDataController.Instance.RuntimeData.CurrentArmor.DefenseMultiplier * 100;
        _armorDefensePower.text = defensePower.ToString();
        _healthValue.text = PlayerDataController.Instance.RuntimeData.CurrentHealth.ToString();
        _sanityValue.text = PlayerDataController.Instance.RuntimeData.CurrentMana.ToString();
    }
    public override void UpdateImages()
    {
        //_characterImage.sprite = _characterSprite;
        _weaponImage.sprite = PlayerDataController.Instance.RuntimeData.CurrentWeapon.WeaponSprite;
        _armorImage.sprite = PlayerDataController.Instance.RuntimeData.CurrentArmor.ArmorSprite;
    }
    public override void UpdateSliders()
    {
        _healthSlider.value = PlayerDataController.Instance.RuntimeData.CurrentHealth /100f;
        _sanitySlider.value = PlayerDataController.Instance.RuntimeData.CurrentMana /100f;
    }
}
