using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static SaveController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetPlayerName(string playerName)
    {
        SaveManager.Instance.Data.PlayerData.PlayerName = playerName;
        SaveManager.Instance.Data.ChoosedNickName = true;
        SaveManager.Instance.Save();
    }
}
