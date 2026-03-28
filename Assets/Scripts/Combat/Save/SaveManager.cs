using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveFile Data { get; private set; }

    string Path => Application.persistentDataPath + "/save.json";

    void Awake()
    {
        Instance = this;
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(Path, json);
    }

    public void Load()
    {
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);
            Data = JsonUtility.FromJson<SaveFile>(json);
        }

        if (Data == null)
        {
            Data = new SaveFile();
            Save();
        }
    }
}
