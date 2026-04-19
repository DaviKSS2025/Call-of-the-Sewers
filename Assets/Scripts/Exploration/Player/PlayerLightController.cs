using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
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
        _inventoryChannel.TorchUsed += TurnOnTorch;
        _gameStateChannel.OnGameStateChange += OnGameStateChange;
    }
    private void OnDisable()
    {
        _inventoryChannel.TorchUsed -= TurnOnTorch;
        _gameStateChannel.OnGameStateChange -= OnGameStateChange;
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
    private void TurnOnTorch(TorchEffect torch)
    {
        _light.intensity = _originalLightIntensity * torch.TorchLightIntensity;
        _currentTorchDuration = torch.TorchDuration;
    }
    private void TurnOffTorch()
    {
        _light.intensity = _originalLightIntensity;
        _inventoryChannel.RaiseTorchEnd();
    }
    private void OnGameStateChange(CurrentGameState gameState)
    {
        isGameplayState = gameState == CurrentGameState.Gameplay ? true : false;
    }
}
