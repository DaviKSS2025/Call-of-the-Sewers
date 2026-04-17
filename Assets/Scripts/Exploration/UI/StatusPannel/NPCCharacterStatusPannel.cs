using UnityEngine;

public class NPCCharacterStatusPannel : BaseCharacterStatusPannel
{
    [SerializeField] private NPCDatabase _database;
    [SerializeField] protected GameObject _statusPannel;

    public override void UpdateCharacterStatus()
    {
        if (DoesCharacterExist())
        {
            _statusPannel.SetActive(true);
            base.UpdateCharacterStatus();
        }
        else
        {
            _statusPannel.SetActive(false);
        }
    }
    public override void UpdateTexts()
    {
        _characterName.text = _database.GetNPCName(NPCDataController.Instance.RuntimeData[0].NPCInfo);

        Weapons npcWeapon = _database.GetNPCWeapon(NPCDataController.Instance.RuntimeData[0].NPCInfo);
        _weaponName.text = npcWeapon.Name;
        int attackPower = (int)npcWeapon.DamageMultiplier * 100;
        _weaponAttackPower.text = attackPower.ToString();

        Armors npcArmor = _database.GetNPCArmor(NPCDataController.Instance.RuntimeData[0].NPCInfo);
        _armorName.text = npcArmor.Name;
        int defensePower = (int)npcArmor.DefenseMultiplier * 100;
        _armorDefensePower.text = defensePower.ToString();

        _healthValue.text = NPCDataController.Instance.RuntimeData[0].CurrentHealth.ToString();
    }
    public override void UpdateImages()
    {
        _characterImage.sprite = _database.GetNPCStatusSprite(NPCDataController.Instance.RuntimeData[0].NPCInfo);

        Weapons npcWeapon = _database.GetNPCWeapon(NPCDataController.Instance.RuntimeData[0].NPCInfo);
        _weaponImage.sprite = npcWeapon.WeaponSprite;

        Armors npcArmor = _database.GetNPCArmor(NPCDataController.Instance.RuntimeData[0].NPCInfo);
        _armorImage.sprite = npcArmor.ArmorSprite;
    }
    public override void UpdateSliders()
    {
        _healthSlider.value = NPCDataController.Instance.RuntimeData[0].CurrentHealth / 100;
    }
    private bool DoesCharacterExist()
    {
        return NPCDataController.Instance.RuntimeData != null && NPCDataController.Instance.RuntimeData.Count > 0;
    }
}
