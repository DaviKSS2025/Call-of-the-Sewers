using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
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
    public void ManualSave()
    {
        Data.PlayerData = PlayerDataController.Instance.RuntimeData;
        Data.NPCData = NPCDataController.Instance.RuntimeData;
        Data.AlreadyRecruitedNPCs = NPCDataController.Instance.NPCHistoric;
        Data.ExplorationData = MapDataController.Instance.GetSaveInfo();
        Data.Items = InventoryDataController.Instance.GetItemList();
        Data.KeyIds = InventoryDataController.Instance.GetKeyIDs();
        Save();
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        File.WriteAllText(Path, json);
    }

    public void LoadOrCreate()
    {
        if (File.Exists(Path))
        {
            try
            {
                string json = File.ReadAllText(Path);
                Data = JsonConvert.DeserializeObject<SaveFile>(json);
            }
            catch
            {
                Debug.LogWarning("Save corrompido. Criando novo.");
                Data = SaveFile.CreateNewGame();
                Save();
                return;
            }
        }

        if (Data == null)
        {
            Data = SaveFile.CreateNewGame();
            Save();
        }
    }

    public void NewGame()
    {
        Data = SaveFile.CreateNewGame();
        Save();
    }

    void ValidateData()
    {
        Data ??= SaveFile.CreateNewGame();

        Data.NPCData ??= new List<AllyNPC>();
        Data.Items ??= new List<ConsumableItemData>();
        Data.KeyIds ??= new List<string>();

        Data.PlayerData ??= new CharacterData();
        Data.ExplorationData ??= new MapExplorationData();
    }
}
