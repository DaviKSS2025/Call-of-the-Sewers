using UnityEngine;

public abstract class InteractionPlace : MonoBehaviour
{
    protected bool isInRange;
    protected bool isInteracted;
    [SerializeField] protected GameStateChannel _gameStateChannel;
    [SerializeField] protected InputChannel _inputChannel;
    [SerializeField] protected DialogueChannel _dialogueChannel;
    protected DialogueStruct[] _beforeInteractionDialogue;
    protected DialogueStruct[] _afterInteractionDialogue;

    public DialogueStruct[] AfterInteractionDialogue
    {
        set => _afterInteractionDialogue = value;
    }

    public virtual void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += ToggleInputs;
    }
    private void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= ToggleInputs;
    }

    public virtual void Start()
    {
        SetupBeforeInteractionDialogue();
        SetupAfterInteractionDialogue();
    }
    protected void ToggleInputs(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            _inputChannel.OnInteract += CallInteraction;
        }
        else
        {
            _inputChannel.OnInteract -= CallInteraction;
        }
    }
    public virtual void CallInteraction()
    {
        if (isInRange)
        {
            _dialogueChannel.OnDialogueEnd += ShowTextAfterInteraction;
            _dialogueChannel.RaiseDialogueRequested(_beforeInteractionDialogue);
            isInteracted = true;
        }
    }
    public virtual void ShowTextAfterInteraction()
    {
        if (!isInteracted)
            return;
        isInteracted = false;
        _dialogueChannel.RaiseDialogueRequested(_afterInteractionDialogue);
        _dialogueChannel.OnDialogueEnd -= ShowTextAfterInteraction;
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
    public abstract void SetupBeforeInteractionDialogue();
    public abstract void SetupAfterInteractionDialogue();
}
