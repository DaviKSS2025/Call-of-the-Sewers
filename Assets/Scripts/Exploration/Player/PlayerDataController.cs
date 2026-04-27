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

    void OnEnable()
    {
        RuntimeData = Clone(SaveManager.Instance.Data.PlayerData);
    }
    public void ApplyDamage(int value)
    {
        RuntimeData.CurrentHealth = Mathf.Max(0, RuntimeData.CurrentHealth - value);
    }
    public void RecoverHealth(int value)
    {
        RuntimeData.CurrentHealth = Mathf.Min(100, RuntimeData.CurrentHealth + value);
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
    public void UseMana(int value)
    {
        RuntimeData.CurrentMana = Mathf.Max(0, RuntimeData.CurrentMana - value);
    }
    public void RecoverMana(int value)
    {
        RuntimeData.CurrentMana = Mathf.Min(100, RuntimeData.CurrentMana + value);
    }
    public void UpdateTorchValues(TorchData data)
    {
        RuntimeData.TorchData = data;
    }
    public TorchData GetTorchValues()
    {
        return RuntimeData.TorchData;
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