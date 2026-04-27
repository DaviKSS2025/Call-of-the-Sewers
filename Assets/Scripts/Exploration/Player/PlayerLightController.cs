using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    private Light2D _light;
    private float _currentTorchDuration;
    private float _originalLightIntensity;
    private bool isGameplayState;
    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _originalLightIntensity = _light.intensity;
    }
    private void OnEnable()
    {
        _inventoryChannel.TorchUsed += TorchLit;
        _gameStateChannel.OnGameStateChange += OnGameStateChange;
        _cutsceneChannel.OnCombatTransitionCutscene += UpdateTorchInfo;
    }
    private void OnDisable()
    {
        _inventoryChannel.TorchUsed -= TorchLit;
        _gameStateChannel.OnGameStateChange -= OnGameStateChange;
        _cutsceneChannel.OnCombatTransitionCutscene -= UpdateTorchInfo;
    }
    private void Start()
    {
        TurnOnTorch(PlayerDataController.Instance.RuntimeData.TorchData.Intensity, PlayerDataController.Instance.RuntimeData.TorchData.RemainingDuration);
    }
    private void Update()
    {
        if (isGameplayState)
        {
            ManageTorchTimer();
        }
    }
    private void ManageTorchTimer()
    {
        if (_currentTorchDuration > 0f)
        {
            _currentTorchDuration -= Time.deltaTime;
        }
        else
        {
            TurnOffTorch();
        }
    }
    private void TorchLit(TorchEffect torch)
    {
        TurnOnTorch(torch.TorchLightIntensity, torch.TorchDuration);
    }
    private void TurnOnTorch(float intensity, float duration)
    {
        _light.intensity = _originalLightIntensity * intensity;
        _currentTorchDuration = duration;
    }
    private void TurnOffTorch()
    {
        _light.intensity = _originalLightIntensity;
        _inventoryChannel.RaiseTorchEnd();
    }
    private void UpdateTorchInfo()
    {
        TorchData currentTorch = new TorchData(_currentTorchDuration, _light.intensity / _originalLightIntensity);
        PlayerDataController.Instance.UpdateTorchValues(currentTorch);
    }
    private void OnGameStateChange(CurrentGameState gameState)
    {
        isGameplayState = gameState == CurrentGameState.Gameplay ? true : false;
    }
}
