using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Colletables : MonoBehaviour
{
    protected bool _insideRange;
    [SerializeField] protected InputChannel _inputChannel;
    [SerializeField] protected GameStateChannel _gameStateChannel;
    protected DialogueStruct[] _dialogueLines;
    [SerializeField] protected DialogueChannel _dialogueChannel;
    protected string _itemName;
    protected string _equipmentType;
    protected string _currentEquipmentName;
    protected bool wasCollected;
    public virtual void OnEnable()
    {
        _gameStateChannel.OnGameStateChange += SwitchInputsOnGameStateChange;
        _dialogueChannel.OnDialogueEnd += DestroyOnDialogueEnd;
    }
    public virtual void OnDisable()
    {
        _gameStateChannel.OnGameStateChange -= SwitchInputsOnGameStateChange;
        _dialogueChannel.OnDialogueEnd -= DestroyOnDialogueEnd;
    }
    public virtual void SwitchInputsOnGameStateChange(CurrentGameState gameState)
    {
        if (gameState == CurrentGameState.Gameplay)
        {
            _inputChannel.OnInteract += OnPlayerPickup;
        }
        else
        {
            _inputChannel.OnInteract -= OnPlayerPickup;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _insideRange = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _insideRange = false;
        }
    }

    public virtual void OnPlayerPickup()
    {
    }
    public virtual string GetCurrentEquipmentName()
    {
        return _currentEquipmentName;
    }
    public virtual void UpgradeEquipment()
    {
        StartThinkingDialogue();
        _dialogueLines[0].DialogueLine = $"I found an stronger {_equipmentType} named <color=red>{_itemName}</color>. I'm sure it'll help me to get out of there.";
        _dialogueChannel.RaiseDialogueRequested(_dialogueLines);
    }
    public virtual void DontPickWorseEquipment()
    {
        StartThinkingDialogue();
        _dialogueLines[0].DialogueLine = $"I found an {_equipmentType} named <color=red>{_itemName}</color>, but its stats are worse than my current <color=red>{_currentEquipmentName}</color>. I'll leave it there.";
        _dialogueChannel.RaiseDialogueRequested(_dialogueLines);
    }
    public virtual void StartThinkingDialogue()
    {
        _dialogueLines = new DialogueStruct[1];

        _dialogueLines[0].SpeakerName = "Thinking";
    }
    public virtual void DestroyOnDialogueEnd()
    {
        if (wasCollected)
        {
            Destroy(gameObject);
        }
    }
}
