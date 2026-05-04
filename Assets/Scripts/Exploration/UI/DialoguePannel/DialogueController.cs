using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialogueChannel _dialogueChannel;
    [SerializeField] private GameObject _dialoguePannel;
    [SerializeField] private TextMeshProUGUI _dialogueTMPro;
    [SerializeField] private TextMeshProUGUI _speakerTMPro;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private GameStateChannel _gameStateChannel;
    private DialogueStruct[] _currentDialogue = null;
    private int _dialogueIndex;
    private void OnEnable()
    {
        _dialogueChannel.DialogueRequested += OnDialogueRequested;
        _gameStateChannel.OnGameStateChange += SwitchDialogueInputs;
    }
    private void OnDisable()
    {
        _dialogueChannel.DialogueRequested -= OnDialogueRequested;
        _gameStateChannel.OnGameStateChange -= SwitchDialogueInputs;
    }
    private void SwitchDialogueInputs(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Dialogue)
        {
            _inputChannel.OnSubmit += AdvanceDialogueIndex;
        }
        else
        {
            _inputChannel.OnSubmit -= AdvanceDialogueIndex;
        }
    }
    private void OnDialogueRequested(DialogueStruct[] dialogue)
    {
        _dialoguePannel.SetActive(true);
        _currentDialogue = dialogue;
        _dialogueIndex = 0;
        UpdateDialogue();
    }
    private void UpdateDialogue()
    {
        if (_currentDialogue != null)
        {
            _dialogueTMPro.text = _currentDialogue[_dialogueIndex].DialogueLine;
            _speakerTMPro.text = _currentDialogue[_dialogueIndex].SpeakerName;
        }
    }
    private void AdvanceDialogueIndex()
    {
        if (_currentDialogue != null )
        {
            if (_dialogueIndex < _currentDialogue.Length - 1)
            {
                _dialogueIndex++;
                UpdateDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }
    private void EndDialogue()
    {
        _currentDialogue = null;
        _dialoguePannel.SetActive(false);
        _dialogueChannel.RaiseDialogueEnd();
    }
}
