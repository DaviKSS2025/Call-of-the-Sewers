using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    [SerializeField] private Weapons _defaultWeapon;
    [SerializeField] private Armors _defaultArmor;
    public SaveFile Data { get; private set; }


    string Path => Application.persistentDataPath + "/save.json";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadOrCreate();
        ValidateData();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(Path, json);
    }

    public void LoadOrCreate()
    {
        if (File.Exists(Path))
        {
            try
            {
                string json = File.ReadAllText(Path);
                Data = JsonUtility.FromJson<SaveFile>(json);
            }
            catch
            {
                Debug.LogWarning("Save corrompido. Criando novo.");
                Data = SaveFile.CreateNewGame(_defaultWeapon, _defaultArmor);
                Save();
                return;
            }
        }

        if (Data == null)
        {
            Data = SaveFile.CreateNewGame(_defaultWeapon, _defaultArmor);
            Save();
        }
    }

    public void NewGame()
    {
        Data = SaveFile.CreateNewGame(_defaultWeapon, _defaultArmor);
        Save();
    }

    void ValidateData()
    {
        if (Data.NPCData == null)
            Data.NPCData = new List<AllyNPC>();

        if (Data.PlayerData == null)
            Data.PlayerData = new CharacterData();
    }
}
