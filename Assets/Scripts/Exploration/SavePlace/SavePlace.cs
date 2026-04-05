using UnityEngine;
public class SavePlace : MonoBehaviour
{
    private bool isInRange;
    private bool isSaving;
    [SerializeField] private GameStateChannel _gameStateChannel;
    [SerializeField] private InputChannel _inputChannel;
    [SerializeField] private DialogueChannel _dialogueChannel;
    private DialogueStruct[] _beforeSaveDialogue;
    private DialogueStruct[] _afterSaveDialogue;

    private void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ToggleInputs;
        _dialogueChannel.OnDialogueEnd += ShowTextAfterSave;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= ToggleInputs;
        _dialogueChannel.OnDialogueEnd -= ShowTextAfterSave;
    }
    private void Start()
    {
        _beforeSaveDialogue = new DialogueStruct[1];

        _beforeSaveDialogue[0].SpeakerName = "Thinking";
        _beforeSaveDialogue[0].DialogueLine = $"Finally, a safe place to rest...";

        _afterSaveDialogue = new DialogueStruct[1];

        _afterSaveDialogue[0].SpeakerName = "System";
        _afterSaveDialogue[0].DialogueLine = $"Game saved.";
    }
    private void ToggleInputs(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            _inputChannel.OnInteract += CallSave;
        }
        else
        {
            _inputChannel.OnInteract -= CallSave;
        }
    }
    private void CallSave()
    {
        if (isInRange)
        {
            _dialogueChannel.RaiseDialogueRequested(_beforeSaveDialogue);
            isSaving = true;
        }
    }
    private void ShowTextAfterSave()
    {
        if (isSaving)
        {
            isSaving = false;
            _dialogueChannel.RaiseDialogueRequested(_afterSaveDialogue);
            SaveManager.Instance.ManualSave();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
