using UnityEngine;
public class PlayerSanityDarknessDrain
{
    private InventoryChannel _inventoryChannel;
    private GameStateChannel _gameStateChannel;
    private DialogueChannel _dialogueChannel;
    private bool isDark = true;
    private bool isUsingTorch;
    private float _maxDarknessCheckTimer = 3f;
    private float _currentDarknessCheckTimer = 3f;
    private int _manaDrain = 1;
    private bool isGameplayState;
    private bool isInRangeToUseMatches;
    private DialogueStruct[] _noLightsAroundDialogue;
    public PlayerSanityDarknessDrain(InventoryChannel inventoryChannel, GameStateChannel gameStateChannel, DialogueChannel dialogueChannel) 
    { 
        _inventoryChannel = inventoryChannel;
        _gameStateChannel = gameStateChannel;
        _dialogueChannel = dialogueChannel;
    }
    public void Initialize()
    {
        _inventoryChannel.TorchActive += UsingTorch;
        _inventoryChannel.TorchEnd += TorchEnd;
        _inventoryChannel.EnteredLightArea += ManageLightArea;
        _inventoryChannel.EnteredEnteredMatchesTriggerArea += DefineRangeToUseMatches;
        _inventoryChannel.MatchesUsed += ShowMessageNoLightSourcesAround;
        _gameStateChannel.OnGameStateChange += ToggleDrainActive;

        _noLightsAroundDialogue = new DialogueStruct[1];

        _noLightsAroundDialogue[0].DialogueLine = "There isn't light sources around.";
        _noLightsAroundDialogue[0].SpeakerName = "Thinking";
    }
    private void UsingTorch()
    {
        isUsingTorch = true;
        _currentDarknessCheckTimer = _maxDarknessCheckTimer;
    }
    private void TorchEnd()
    {
        isUsingTorch = false;
    }
    private void ManageLightArea(bool isEntering)
    {
        isDark = !isEntering;
        _currentDarknessCheckTimer = _maxDarknessCheckTimer;
    }
    private void ToggleDrainActive(CurrentGameState gameState)
    {
        isGameplayState = gameState == CurrentGameState.Gameplay ? true : false;
    }
    public void CheckDarkness()
    {
        if (isGameplayState)
        {
            if (isDark && !isUsingTorch)
            {
                if (_currentDarknessCheckTimer > 0)
                {
                    _currentDarknessCheckTimer -= Time.deltaTime;
                }
                else
                {
                    _currentDarknessCheckTimer = _maxDarknessCheckTimer;
                    PlayerDataController.Instance.UseMana(_manaDrain);
                }
            }
        }
    }
    private void DefineRangeToUseMatches(bool isEnteringInRange)
    {
        isInRangeToUseMatches = isEnteringInRange;
    }
    private void ShowMessageNoLightSourcesAround()
    {
        if (!isInRangeToUseMatches)
        {
            _dialogueChannel.RaiseDialogueRequested(_noLightsAroundDialogue);
        }
        else
        {
            _gameStateChannel.RaiseGameStateChange(CurrentGameState.Gameplay);
        }
    }
    public void OnDisable()
    {
        _inventoryChannel.TorchActive -= UsingTorch;
        _inventoryChannel.TorchEnd -= TorchEnd;
        _inventoryChannel.EnteredLightArea -= ManageLightArea;
        _inventoryChannel.EnteredEnteredMatchesTriggerArea -= DefineRangeToUseMatches;
        _inventoryChannel.MatchesUsed -= ShowMessageNoLightSourcesAround;
        _gameStateChannel.OnGameStateChange -= ToggleDrainActive;
    }
}
