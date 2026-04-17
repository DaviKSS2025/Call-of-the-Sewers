using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private CutsceneChannel _cutsceneChannel;
    [SerializeField] private CurrentGameState _gameState = CurrentGameState.Gameplay;
    private void OnEnable()
    {
        _dialogueChannel.OnDialogueStart += OnDialogueStart;
        _dialogueChannel.OnDialogueEnd += OnDialogueEnd;
        _inputChannel.OnMenuToggle += OnMenuToggle;
        _cutsceneChannel.OnBlackoutRequested += OnCutsceneStart;
    }
    private void OnDisable()
    {
        _dialogueChannel.OnDialogueStart -= OnDialogueStart;
        _dialogueChannel.OnDialogueEnd -= OnDialogueEnd;
        _inputChannel.OnMenuToggle -= OnMenuToggle;
        _cutsceneChannel.OnBlackoutRequested -= OnCutsceneStart;
    }
    private void Start()
    {
        ChangeGameState(CurrentGameState.Gameplay);
    }
    private void OnDialogueStart()
    {
        ChangeGameState(CurrentGameState.Dialogue);
    }
    private void OnDialogueEnd()
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
            ChangeGameState(CurrentGameState.Gameplay);
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
}
public enum CurrentGameState
{
    Gameplay,
    StatusPannel,
    Dialogue,
    Cutscene
}
