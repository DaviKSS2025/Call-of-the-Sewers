using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public static MapDataController Instance;
    [SerializeField] private SceneChangeChannel _sceneChangeChannel;
    public struct PositionData 
    {
        public Vector2 WorldPosition;
        public SceneNames CurrentSceneName;
        public bool UsedSacrificePlace;
    }
    public PositionData RuntimeData
    {
        get; private set;
    }
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
    }
    private void OnDisable()
    {
        _sceneChangeChannel.GoToTargetScene -= UpdateSceneNameOnChange;
    }
    private void UpdateSceneNameOnChange(SceneNames nextScene)
    {
        var data = RuntimeData;
        data.CurrentSceneName = nextScene;
        RuntimeData = data;
    }
    public Vector2 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var data = RuntimeData;
        data.WorldPosition = player.transform.position;
        RuntimeData = data;
        return RuntimeData.WorldPosition;
    }
    public void UsedSacrificePlace()
    {
        var data = RuntimeData;
        data.UsedSacrificePlace = true;
        RuntimeData = data;
    }
    void Start()
    {
        RuntimeData = Clone(SaveManager.Instance.Data.WorldPosition, SaveManager.Instance.Data.CurrentMapName, SaveManager.Instance.Data.UsedSacrificePlace);
    }
    private PositionData Clone(Vector2 originalPosition, SceneNames originalScene, bool usedSacrificePlace)
    {
        return new PositionData
        {
            WorldPosition = originalPosition,
            CurrentSceneName = originalScene,
            UsedSacrificePlace = usedSacrificePlace
        };
    }
}
