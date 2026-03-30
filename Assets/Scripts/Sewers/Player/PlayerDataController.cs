using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController Instance;

    public CharacterData RuntimeData { get; private set; }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        RuntimeData = Clone(SaveManager.Instance.Data.PlayerData);
    }

    public void ApplyDamage(int value)
    {
        RuntimeData.CurrentHealth -= value;
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
            CurrentWeapon = original.CurrentWeapon
        };
    }
}