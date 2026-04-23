using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public static MapDataController Instance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;

    public MapExplorationData RuntimeExplorationData;
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
    private void OnEnable()
    {
        _sceneChangeChannel.GoToTargetScene += UpdateSceneNameOnChange;
        RuntimeExplorationData = SaveManager.Instance.Data.ExplorationData;
    }
    private void OnDisable()
    {
        _sceneChangeChannel.GoToTargetScene -= UpdateSceneNameOnChange;
    }
    private void UpdateSceneNameOnChange(SceneNames nextScene)
    {
        RuntimeExplorationData.CurrentMapName = nextScene;
    }
    public Vector2 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RuntimeExplorationData.WorldPosX = player.transform.position.x;
        RuntimeExplorationData.WorldPosY = player.transform.position.y;
        return new Vector2(RuntimeExplorationData.WorldPosX, RuntimeExplorationData.WorldPosY);
    }
    public void UsedSacrificePlace()
    {
        RuntimeExplorationData.UsedSacrificePlace = true;
    }
    public void OpenDoor(string doorName)
    {
        RuntimeExplorationData.OpenedDoors[doorName] = true;
    }
    public void LightCandle(string candleID, bool state)
    {
        RuntimeExplorationData.LitCandles[candleID] = state;
    }
    public MapExplorationData GetSaveInfo()
    {
        GetPlayerPosition();
        return RuntimeExplorationData;
    }
}
