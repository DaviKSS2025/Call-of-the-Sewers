using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        Data.WorldPosition = MapDataController.Instance.GetPlayerPosition();
        Data.CurrentMapName = MapDataController.Instance.RuntimeData.CurrentSceneName;
        Data.Items = InventoryDataController.Instance.GetItemList();
        Save();
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
        if (Data.NPCData == null)
            Data.NPCData = new List<AllyNPC>();

        if (Data.PlayerData == null)
            Data.PlayerData = new CharacterData();
    }
}
