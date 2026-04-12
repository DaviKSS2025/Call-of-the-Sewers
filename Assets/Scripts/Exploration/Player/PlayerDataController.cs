using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController Instance;

    public CharacterData RuntimeData { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RuntimeData = Clone(SaveManager.Instance.Data.PlayerData);
    }

    public void ApplyDamage(int value)
    {
        RuntimeData.CurrentHealth -= value;
    }
    public void UpgradeWeapon(WeaponType weapon)
    {
        RuntimeData.CurrentWeapon = weapon;
    }
    public void UpgradeArmor(ArmorType armor)
    {
        RuntimeData.CurrentArmor = armor;
    }
    public void AddSkill(SkillType skill)
    {
        RuntimeData.SkillList.Add(skill);
    }

    public void Save()
    {
        SaveManager.Instance.Data.PlayerData = RuntimeData;
        SaveManager.Instance.Save();
    }
    private CharacterData Clone(CharacterData original)
    {
        return new CharacterData
        {
            PlayerName = original.PlayerName,
            CurrentHealth = original.CurrentHealth,
            CurrentMana = original.CurrentMana,
            CurrentArmor = original.CurrentArmor,
            CurrentWeapon = original.CurrentWeapon,
            SkillList = original.SkillList
        };
    }
}