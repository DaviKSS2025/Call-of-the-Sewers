using UnityEngine;
using System.Diagnostics;
public class GameStateController : MonoBehaviour
{
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] private ChoiceChannel _choiceChannel;
    [SerializeField] private InventoryChannel _inventoryChannel;
    [SerializeField] private CurrentGameState _gameState = CurrentGameState.Gameplay;
    private void OnEnable()
    {
        _dialogueChannel.OnDialogueStart += OnDialogueStart;
        _dialogueChannel.OnDialogueEnd += EnableGameplay;
        _inputChannel.OnMenuToggle += OnMenuToggle;
        _cutsceneChannel.OnBlackoutRequested += OnCutsceneStart;
        _cutsceneChannel.OnHalfBlackoutRequested += OnCutsceneStart;
        _choiceChannel.ChoiceRequested += OnChoiceStart;
        _inventoryChannel.InstantItemUsed += EnableGameplay;
    }
    private void OnDisable()
    {
        _dialogueChannel.OnDialogueStart -= OnDialogueStart;
        _dialogueChannel.OnDialogueEnd -= EnableGameplay;
        _inputChannel.OnMenuToggle -= OnMenuToggle;
        _cutsceneChannel.OnBlackoutRequested -= OnCutsceneStart;
        _cutsceneChannel.OnHalfBlackoutRequested -= OnCutsceneStart;
        _choiceChannel.ChoiceRequested -= OnChoiceStart;
        _inventoryChannel.InstantItemUsed -= EnableGameplay;
    }
    private void Start()
    {
        EnableGameplay();
    }
    private void OnDialogueStart()
    {
        ChangeGameState(CurrentGameState.Dialogue);
    }
    private void EnableGameplay()
    {
        ChangeGameState(CurrentGameState.Gameplay);
    }
    private void OnMenuToggle()
    {
        if (_gameState == CurrentGameState.Gameplay)
        {
            ChangeGameState(CurrentGameState.StatusPannel);
        }
        else if (_gameState == CurrentGameState.StatusPannel)
        {
            EnableGameplay();
        }
    }
    private void ChangeGameState(CurrentGameState gameState)
    {
        _gameState = gameState;
        _gameStateChannel.RaiseGameStateChange(gameState);
    }
    private void OnCutsceneStart()
    {
        ChangeGameState(CurrentGameState.Cutscene);
    }
    private void OnChoiceStart()
    {
        ChangeGameState(CurrentGameState.Choice);
    }
}
public enum CurrentGameState
{
    Gameplay,
    StatusPannel,
    Dialogue,
    Cutscene,
    Choice
}
